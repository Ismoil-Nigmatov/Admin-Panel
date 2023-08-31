using Microsoft.AspNetCore.Mvc;
using MVC.Entity;
using MVC.Repository;

namespace MVC.Controllers
{
	public class EducationController : Controller
	{
		private readonly IEducationRepository _educationRepository;
		private readonly ICourseRepository _courseRepository;

		public EducationController(IEducationRepository educationRepository, ICourseRepository courseRepository)
		{
			_educationRepository = educationRepository;
			_courseRepository = courseRepository;
		}
		public async Task<IActionResult> GetEducationList()
		{
			var allEducations = await _educationRepository.GetAll();

			return View("EducationTable", allEducations);
		}
		public async Task<IActionResult> AddEducation()
		{
			ViewBag.CourseList = await _courseRepository.GetAll();
			return View("AddEducation");
		}
		[HttpPost]
		public async Task<IActionResult> AddEducation(string title , string end , string description , int courseId)
		{
			Education education = new Education();
			education.Title = title;
			education.End = end;
			education.Description = description;
			education.Course = await _courseRepository.Get(courseId);
			await _educationRepository.Add(education);
			var task = await _educationRepository.GetAll();
			return View("EducationTable", task);
		}
		[HttpPost]
		public async Task<IActionResult> GetEducationById(int id)
		{
			var education = await _educationRepository.Get(id);
			ViewBag.CourseList = await _courseRepository.GetAll();
			return View("UpdateEducation", education);
		}
		[HttpPost]
		public async Task<IActionResult> UpdateEducation(int id, string title, string end, string description, int courseId)
		{
			var education = await _educationRepository.Get(id);
			education.Title = title;
			education.End = end;
			education.Description = description;
			education.Course = await _courseRepository.Get(courseId);
			await _educationRepository.Update(education);
			var task = await _educationRepository.GetAll();
			return View("EducationTable", task);
		}
		public async Task<IActionResult> UpdateEducation()
		{
			return View("GetById");
		}
		public async Task<IActionResult> DeleteEducation()
		{
			return View("DeleteEducation");
		}
		[HttpPost]
		public async Task<IActionResult> DeleteEducation(int id)
		{
			await _educationRepository.Delete(id);
			var educations = await _educationRepository.GetAll();
			return View("EducationTable", educations);
		}
	}
}
