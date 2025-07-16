using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using YodMS.Authorization;
using YodMS.Models;
using YodMS.Models.DataBase_Manager;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

/////////// 
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("OwnDocWrite", policy =>
        policy.Requirements.Add(new OwnDocumentRequirement()));  // Handler ??????

    options.AddPolicy("FinanceReview",
        policy => policy.RequireRole("President", "Secretary-General", "FinanceOfficer"));

    options.AddPolicy("OpenVoteSession",
        policy => policy.RequireRole("President", "Secretary-General"));

    options.AddPolicy("CanVote",
        policy => policy.RequireAssertion(ctx =>
            !ctx.User.IsInRole("Auditor"))); // ?? ??????? ??? ???????
});
builder.Services.AddSingleton<IAuthorizationHandler, OwnDocumentHandler>();

///////////
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(opts =>
    {
        opts.LoginPath = "/Account/Login";
        opts.LogoutPath = "/Account/Logout";
        opts.AccessDeniedPath = "/Account/Login";
    });

builder.Services.AddAuthorization();   // ???????? ???? ???????? ??????



// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
