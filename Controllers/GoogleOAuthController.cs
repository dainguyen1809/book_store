using System;
using System.Threading;
using System.Threading.Tasks;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Auth.OAuth2.Flows;
using Google.Apis.Auth.OAuth2.Mvc;
using Google.Apis.Oauth2.v2;
using Google.Apis.Oauth2.v2.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Book_Store.Repository.Data;
using Book_Store.Models;
using Microsoft.AspNetCore.Identity;
using Book_Store.Repository;
using Book_Store.Models.ViewModels;

[Controller]
public class GoogleOAuthController : Controller
{
	private static readonly string[] Scopes = { Oauth2Service.Scope.UserinfoProfile, Oauth2Service.Scope.UserinfoEmail };
	private readonly DataContext _db;
	private UserManager<AppCustomer> _userManager;

	public GoogleOAuthController(DataContext db, UserManager<AppCustomer> userManager)
	{
		_db = db;
		_userManager = userManager;

	}

	[HttpGet]
	[Route("GoogleOAuth/SignIn")]
	public ActionResult SignIn()
	{
		var authCallbackUri = new Uri(Url.Action(nameof(HandleGoogleCallbackAsync), "GoogleOAuth", null, Request.Scheme));
		var flow = new GoogleAuthorizationCodeFlow(new GoogleAuthorizationCodeFlow.Initializer
		{
			ClientSecrets = new ClientSecrets
			{
				ClientId = "296417493435-t47tqrng3nsqb4cbu8khee02cb1582fn.apps.googleusercontent.com",
				ClientSecret = "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"
			},
			Scopes = Scopes,
		});

		var authUri = flow.CreateAuthorizationCodeRequest(authCallbackUri.ToString()).Build();
		return new RedirectResult(authUri.ToString());
	}


	[HttpGet]
	[Route("GoogleOAuth/HandleGoogleCallbackAsync")]
	public async Task<ActionResult> HandleGoogleCallbackAsync(string code, CancellationToken cancellationToken)
	{


		var authCallbackUri = Url.Action(nameof(HandleGoogleCallbackAsync), "GoogleOAuth", null, Request.Scheme);
		var flow = new GoogleAuthorizationCodeFlow(new GoogleAuthorizationCodeFlow.Initializer
		{
			ClientSecrets = new ClientSecrets
			{
				ClientId = "296417493435-t47tqrng3nsqb4cbu8khee02cb1582fn.apps.googleusercontent.com",
				ClientSecret = "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"
			},
			Scopes = Scopes,
		});

		var token = await flow.ExchangeCodeForTokenAsync(User.Identity.Name, code, authCallbackUri, cancellationToken);

		// Now you can use the token to make authenticated requests to Google APIs
		var userInfo = await new Oauth2Service(new Google.Apis.Services.BaseClientService.Initializer
		{
			HttpClientInitializer = GoogleCredential.FromAccessToken(token.AccessToken)
		}).Userinfo.Get().ExecuteAsync();

		// Example: Retrieve user information
		var userId = userInfo.Id;
		var userEmail = userInfo.Email;
		var userName = userInfo.Name;

		var user = await _userManager.FindByEmailAsync(userEmail);
		if(user == null)
		{
			return Redirect("/Account/Register");
		}

		HttpContext.Session.SetJson("UserId", user.Id);
		
		return Redirect("/");
	}

	[HttpGet]
	[Route("GoogleOAuth/SignOut")]
	public ActionResult SignOut()
	{
		HttpContext.Session.Remove("UserId");

		return Redirect("/");
	}
}