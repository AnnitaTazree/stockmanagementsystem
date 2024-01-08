using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http.Abstractions;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.FileSystemGlobbing.Internal.Patterns;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddAuthentication(
CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(option => {
        option.LoginPath = "/Access/Index";
    option.ExpireTimeSpan = TimeSpan.FromMinutes(20);


    });

var app = builder.Build();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();

app.UseAuthorization();

app.MapRazorPages();
    //name: "default",
   // PatternBuilder: "{razorpages=Access}/{action=Index}/{id}");


app.Run();
