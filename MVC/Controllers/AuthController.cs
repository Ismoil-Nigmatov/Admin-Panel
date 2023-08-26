using Microsoft.AspNetCore.Mvc;
using MVC.Dto;
using MVC.Entity;
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
                var user = await _authService.GetUserName(loginDto.Email);
                var allUser = await _authService.GetAllUser();
                TempData["User"] = JsonConvert.SerializeObject(user);
                TempData["Users"] = JsonConvert.SerializeObject(allUser);
                return RedirectToAction("Dashboard", "Dashboard");
            }

            return RedirectToAction("Login", "Auth");
        }

    }
}