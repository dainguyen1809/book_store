using Book_Store.Models;
using Book_Store.Repository.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//API
builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();
builder.Services.AddHttpClient();

// Connection DB
builder.Services.AddDbContext<DataContext>(
    option => option.UseSqlServer(builder.Configuration.GetConnectionString("BookStore"))
    );

// Save cache
builder.Services.AddDistributedMemoryCache();

// Add Session
builder.Services.AddSession(
    options => {
        options.IdleTimeout = TimeSpan.FromMinutes(30);
        options.Cookie.IsEssential = true;
        }
    );



//Identity Customers
builder.Services.AddIdentity<AppCustomer, IdentityRole>(/* Xác thực tài khoản qua Email ---->> options => options.SignIn.RequireConfirmedAccount = true*/)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<DataContext>().AddDefaultTokenProviders();

builder.Services.Configure<IdentityOptions>(options =>
{
    // Password settings.
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequiredLength = 6;
    //  Yêu cầu ký tự đặt biệt ---->> options.Password.RequiredUniqueChars = 1;

    /*
        // Lockout settings.
        options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
        options.Lockout.MaxFailedAccessAttempts = 5;
        options.Lockout.AllowedForNewUsers = true;

        // User settings.
        options.User.AllowedUserNameCharacters =
        "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
        options.User.RequireUniqueEmail = false;
    */
});

//  Role Admin
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Account/SignIn";
});


//  Authentication Facebook
//builder.Services.AddAuthentication().AddFacebook(facebookOptions =>
//{
//    facebookOptions.ClientId = "1775825096164187";
//    facebookOptions.ClientSecret = "e8b616eeacdedd592e3259c382022799";
//});


/* ------------------------------------------------------------------------------------------------------------------------------ */

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}


// Use Session
app.UseSession();


app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// Xác thực và phân quyền
app.UseAuthentication();
app.UseAuthorization();

app.UseAuthorization();



// Admin

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
      name: "areas",
      pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
    );
});

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
	name: "api",
	pattern: "{api}/{controller}/{id?}");

app.Run();
