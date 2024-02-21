using Microsoft.AspNetCore.Mvc;
using PayPal.Api;
using System.Collections.Generic;

public class PayPalController : Controller
{
	private readonly string clientId = "AfdT35N7U4ONak13bE5ZiSXtzIzLlwNz93_k2aKCEMYBWicE4eexgXFnHvjLNE3itmwpcXxkEYMJzTEQ";
	private readonly string clientSecret = "EBDALI8S4FTsQMpIJNKW7LwlqLkI94mVGTKLzUX75_yMeueRUktO0S5fIFQMx03d6TlTuSXjYJXvmYNV";

	public IActionResult Index()
	{
		return View();
	}

	public IActionResult CreatePayment()
	{
		// Check if HttpContext is not null
		if (HttpContext != null)
		{
			var apiContext = GetAPIContext();

			var payment = Payment.Create(apiContext, new Payment
			{
				intent = "sale",
				payer = new Payer { payment_method = "paypal" },
				transactions = new List<Transaction>
				{
					new Transaction
					{
						description = "Transaction description",
						amount = new Amount
						{
							currency = "USD",
							total = "10.00" // Replace with your amount
                        }
					}
				},
				redirect_urls = new RedirectUrls
				{
					return_url = Url.Action("PaymentSuccess", "PayPal", null, HttpContext.Request.Scheme),
					cancel_url = Url.Action("PaymentCancelled", "PayPal", null, HttpContext.Request.Scheme)
				}
			});

			return Redirect(payment.GetApprovalUrl());
		}
		else
		{
			// Handle the case when HttpContext is null
			// This could be an error or a fallback behavior
			return RedirectToAction("Index");
		}
	}

	public IActionResult PaymentSuccess(string paymentId, string token, string PayerID)
	{
		var apiContext = GetAPIContext();

		var paymentExecution = new PaymentExecution { payer_id = PayerID };
		var payment = new Payment { id = paymentId };

		var executedPayment = payment.Execute(apiContext, paymentExecution);

		// Perform actions after successful payment
		return Redirect("/Order/Checkout");
	}

	public IActionResult PaymentCancelled()
	{
		// Handle payment cancellation
		return Redirect("/");
	}

	private APIContext GetAPIContext()
	{
		var config = new Dictionary<string, string>
		{
			{"mode", "sandbox"}, // Change to "live" for production
            {"clientId", clientId},
			{"clientSecret", clientSecret}
		};

		var accessToken = new OAuthTokenCredential(config).GetAccessToken();
		return new APIContext(accessToken) { Config = config };
	}
}
