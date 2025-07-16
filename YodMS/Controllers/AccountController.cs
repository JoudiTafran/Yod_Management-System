using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using YodMS.Models;
using YodMS.Models.DataBase_Manager;
using Claim = System.Security.Claims.Claim;


namespace YodMS.Controllers;

public class AccountController : Controller
{
    private readonly AppDbContext _db;

    public AccountController(AppDbContext db) => _db = db;

    // GET: /Account/Login
    [HttpGet, AllowAnonymous]
    public IActionResult Login(string? returnUrl = null)
    {
        ViewBag.ReturnUrl = returnUrl;
        return View();
    }

    // POST: /Account/Login
    [HttpPost, ValidateAntiForgeryToken, AllowAnonymous]
    public async Task<IActionResult> Login(LoginViewModel vm, string? returnUrl = null)
    {
        if (!ModelState.IsValid) return View(vm);

        var user = _db.Users.SingleOrDefault(u => u.Username == vm.UserName);
        if (user == null || !VerifyPassword(vm.Password, user.PasswordHash))
        {
            ModelState.AddModelError(string.Empty, "بيانات الدخول غير صحيحة.");
            return View(vm);
        }

        // إنشاء قائمة Claims
        var claims = new List<Claim>
{
    new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
    new Claim(ClaimTypes.Name, user.Username),
    new Claim(ClaimTypes.Role, user.Role)  // President, Auditor, …
};


        var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        var principal = new ClaimsPrincipal(identity);

        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal,
            new AuthenticationProperties { IsPersistent = vm.RememberMe });

        return LocalRedirect(returnUrl ?? Url.Action("Index", "Home")!);
    }

    // GET: /Account/Logout
    [Authorize]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToAction(nameof(Login));
    }

    // -------- وسائل مساعدة ------------------------------------------------

    private static bool VerifyPassword(string inputPassword, string storedHash)
    {
        // استبدل هذا بتحقق Bcrypt/Argon2 إن كانت كلمات المرور مشفّرة بتلك الخوارزميات
        var sha = SHA256.HashData(Encoding.UTF8.GetBytes(inputPassword));
        var hash = Convert.ToHexString(sha);
        return hash == storedHash;
    }
}

/// <summary>نموذج View للّوجين</summary>
public class LoginViewModel
{
    [System.ComponentModel.DataAnnotations.Required]
    public string UserName { get; set; } = default!;

    [System.ComponentModel.DataAnnotations.Required, System.ComponentModel.DataAnnotations.DataType(System.ComponentModel.DataAnnotations.DataType.Password)]
    public string Password { get; set; } = default!;

    public bool RememberMe { get; set; }
}
