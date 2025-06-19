using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using CakeApp.Models;
using Services;

namespace CakeApp.Controllers;

public class HomeController : Controller
{
    private readonly HomeService _service;

    public HomeController(HomeService service)
    {
        _service = service;
    }

    public IActionResult Login()
    {
        if (HttpContext.Session.GetString("user") == null)
        {
            return View();
        }
        else
        {
            return RedirectToAction("Index", "Home");
        }
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginViewModel user)
    {
        if (ModelState.IsValid)
        {
            bool checkUser = await _service.CheckUserAsync(user);
            if (!checkUser)
            {
                TempData["notPresent"] = "No user found!";
                return View();
            }
            Users match = await _service.LoginUserAsync(user);
            if (match == null)
            {
                TempData["noMatch"] = "Email and Password doesn't match";
                return View();
            }
            HttpContext.Session.SetString("user", match.FirstName);
            ViewBag.user = HttpContext.Session.GetString("user");
            //HttpContext.Session.SetString("user", match.FirstName);
            return RedirectToAction("Index", "Home");
        }
        else
        {
            return View(user);
        }
    }
    public IActionResult Index()
    {
        if (HttpContext.Session.GetString("user") != null)
        {
            return View();
        }
        else
        {
            return RedirectToAction("Login", "Home");
        }
    }
    [HttpGet]
    public IActionResult Register()
    {
        if (HttpContext.Session.GetString("user") == null)
        {
            return View();
        }
        else
        {
            return RedirectToAction("Index", "Home");
        }
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Register(Users newUSer)
    {
        if (ModelState.IsValid)
        {
            LoginViewModel user = new LoginViewModel()
            {
                Email = newUSer.Email,
                Password = newUSer.Password
            };
            bool check = await _service.CheckUserAsync(user);
            if (check)
            {
                TempData["exist"] = "Email Already exits";
                return View(newUSer);
            }
            await _service.CreateUserAsync(newUSer);
            TempData["register"] = " Registered";
            return RedirectToAction("Login", "Home");
        }
        else
        {
            return View(newUSer);
        }
    }
    public IActionResult Logout()
    {
        return View();
    }

    public IActionResult ConfirmLogout()
    {
        if (HttpContext.Session.GetString("user") != null)
        {
            HttpContext.Session.Remove("user");
        }
        return View("Login");
    }
    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
