using Microsoft.AspNetCore.Mvc;
using MVC.Entity;
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


		public async Task<IActionResult> GetTeacherAdd()
		{
			return View("_AddTeacher");
		}
		[HttpPost]
		public async Task<IActionResult> AddTeacher(string fullName, string imgUrl, string type)
		{
			Teacher teacher = new Teacher();
			teacher.Name = fullName;
			teacher.ImageUrl = imgUrl;
			teacher.Type = type;
			await _teacherRepository.AddTeacherAsync(teacher);

			var allTeacherAsync = await _teacherRepository.GetAllTeacherAsync();
			return View("_TeacherCard", allTeacherAsync);
		}

		[HttpPost]
		public async Task<IActionResult> GetTeacherById(int id)
		{
			var teacherByIdAsync = await _teacherRepository.GetTeacherByIdAsync(id);
			return View("_UpdateTeacher", teacherByIdAsync);
		}

		public async Task<IActionResult> GetTeacherById()
		{
			return View("_GetTeacher");
		}
		[HttpPost]
		public async Task<IActionResult> UpdateTeacher(int id, string name, string imgUrl ,string type)
		{
			Teacher teacher = new Teacher();
			teacher.Name = name;
			teacher.ImageUrl = imgUrl;
			teacher.Type = type;
			teacher.Id = id;
			await _teacherRepository.UpdateTeacherAsync(teacher);
			var allTeacherAsync = await _teacherRepository.GetAllTeacherAsync();
			return View("_TeacherCard", allTeacherAsync);
		}

		public async Task<IActionResult> DeleteTeacher()
		{
			return View("_DeleteTeacher");
		}

		[HttpPost]
		public async Task<IActionResult> DeleteTeacher(int id)
		{
			await _teacherRepository.DeleteTeacherAsync(id);
			var allTeacherAsync = await _teacherRepository.GetAllTeacherAsync();
			return View("_TeacherCard", allTeacherAsync);
		}
	}
}
