using Microsoft.AspNetCore.Mvc;
using MVC.Dto;
using MVC.Entity;
using MVC.Models;
using MVC.Service;
using Newtonsoft.Json;

namespace MVC.Controllers
{
    public class AuthController : Controller
    {

        private readonly AuthService _authService;

        public AuthController(AuthService authService)
        {
            _authService = authService;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Login(LoginDTO loginDto)
        {
            if (!ModelState.IsValid)
                return View(loginDto);

            bool login = _authService.Login(loginDto);

            if (login)
            {

	            return RedirectToAction("GetUserList", "User");
            }

            return RedirectToAction("Login", "Auth");
        }

    }
}