
using Microsoft.AspNetCore.Mvc;
using MVC.Dto;
using MVC.Entity;
using MVC.Repository;
using Newtonsoft.Json;

namespace MVC.Controllers
{
	public class UserController : Controller
	{
		public readonly IUserRepository UserRepository;
		private readonly IFeedbackRepository _feedbackRepository;
		private readonly IResultRepository _resultRepository;

		public UserController(IUserRepository userRepository, IFeedbackRepository feedbackRepository, IResultRepository resultRepository)
		{
			UserRepository = userRepository;
			_feedbackRepository = feedbackRepository;
			_resultRepository = resultRepository;
		}

		public async Task<IActionResult> DeleteUser(int id)
		{
			await UserRepository.DeleteUserAsync(id);
			var allUsersAsync = await UserRepository.GetAllUsersAsync();
			TempData["Users"] = JsonConvert.SerializeObject(allUsersAsync);
			return RedirectToAction("Dashboard", "Dashboard");
		}

        [HttpPost]
        public async Task<IActionResult> UpdateUser(int id, string newFullName, string newEmail)
        {
            UserDTO userDto = new UserDTO();
			userDto.FullName = newFullName;
            userDto.Email = newEmail;
            await UserRepository.UpdateUserAsync(id , userDto);
            var allUsersAsync = await UserRepository.GetAllUsersAsync();
            TempData["Users"] = JsonConvert.SerializeObject(allUsersAsync);
			return RedirectToAction("Dashboard", "Dashboard");
        }
        
        [HttpPost]
        public async Task<IActionResult> AddUser(string fullName, string newEmail, string newPassword)
        {
	        Entity.User newUser = new User();
			newUser.FullName = fullName;
			newUser.Email = newEmail;
			newUser.Password = newPassword;
			await UserRepository.AddUserAsync(newUser);
            var allUsersAsync = await UserRepository.GetAllUsersAsync();
            TempData["Users"] = JsonConvert.SerializeObject(allUsersAsync);
			return RedirectToAction("Dashboard", "Dashboard");
        }

        [HttpPost]
        public async Task<IActionResult> GetUserCourses(int id)
        {
	        var userCourses = await UserRepository.GetUserCourses(id);

			return PartialView("_UserCourseTable", userCourses);
        }

        [HttpPost]
        public async Task<IActionResult> GetUserFeedbacks(int id)
        {
	        var userFeedbacks = await _feedbackRepository.GetUserFeedbacks(id);

	        return PartialView("_UserFeedbackTable", userFeedbacks);
        }

        [HttpPost]
        public async Task<IActionResult> GetUserResults(int id)
        {
	        var userResults = await _resultRepository.GetUserResult(id);

	        return PartialView("_UserResultTable", userResults);
        }
	}
}
