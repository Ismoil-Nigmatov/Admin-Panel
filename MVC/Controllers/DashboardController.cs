using Microsoft.AspNetCore.Mvc;
using MVC.Dto;
using MVC.Entity;
using MVC.Models;
using Newtonsoft.Json;

namespace MVC.Controllers
{
    public class DashboardController : Controller
    {
        public ActionResult Dashboard()
        {
	        DashboardViewModel viewModel = new DashboardViewModel();

			string userNameJson = TempData["User"].ToString(); 
            var user = JsonConvert.DeserializeObject<User>(userNameJson);
            var usersJson = TempData["Users"].ToString();
            List<User> users = JsonConvert.DeserializeObject<List<User>>(usersJson);

            TempData.Keep("User");
	        TempData.Keep("Users"); 
	        
	        viewModel.CurrentUser = user; 
	        viewModel.AllUsers = users; 

	        return View(viewModel);
        }
    }
}
