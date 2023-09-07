using Microsoft.AspNetCore.Mvc;
using MVC.Dto;
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
		public async Task<IActionResult> AddTeacher(TeacherDTO teacherDto)
		{
			Teacher teacher = new Teacher();
			teacher.Name = teacherDto.Name;
			teacher.ImageUrl = teacherDto.imgUrl;
			teacher.Type = teacherDto.Type;
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
		public async Task<IActionResult> UpdateTeacher(TeacherDTO teacherDto)
		{
			Teacher teacher = new Teacher();
			teacher.Name = teacherDto.Name;
			teacher.ImageUrl = teacherDto.imgUrl;
			teacher.Type = teacherDto.Type;
			teacher.Id = teacherDto.Id;
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
