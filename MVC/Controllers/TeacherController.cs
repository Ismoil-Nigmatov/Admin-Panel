using Microsoft.AspNetCore.Mvc;
using MVC.Repository;
using MVC.Repository.Impl;

namespace MVC.Controllers
{
	public class TeacherController : Controller
	{
		private readonly ITeacherRepository _teacherRepository;

		public TeacherController(ITeacherRepository teacherRepository)
		{
			_teacherRepository = teacherRepository;
		}

		public async Task<IActionResult> GetTeacherList()
		{
			var allTeacherAsync = await _teacherRepository.GetAllTeacherAsync();

			return View("_TeacherCard", allTeacherAsync);
		}
	}
}
