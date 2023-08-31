using Microsoft.AspNetCore.Mvc;
using MVC.Entity;
using MVC.Repository;
using MVC.Repository.Impl;

namespace MVC.Controllers
{
	public class LessonController : Controller
	{
		private readonly ILessonRepository _lessonRepository;
		private readonly ICourseRepository _courseRepository;

		public LessonController(ILessonRepository lessonRepository, ICourseRepository courseRepository)
		{
			_lessonRepository = lessonRepository;
			_courseRepository = courseRepository;
		}

		public async Task<IActionResult> GetLessons()
		{
			var allLessonAsync = await _lessonRepository.GetAllLessonAsync();

			return View("LessonTable", allLessonAsync);
		}

		public async Task<IActionResult> AddLesson()
		{
			ViewBag.CourseList = await _courseRepository.GetAll();
			return View("AddLesson");
		}

		[HttpPost]
		public async Task<IActionResult> AddLesson(string title, string videoUrl, string information , int courseId)
		{
			Lesson lesson = new Lesson();
			lesson.Title = title;
			lesson.VideoUrl = videoUrl;
			lesson.Information = information;
			lesson.Course = await _courseRepository.Get(courseId);
			await _lessonRepository.AddLessonAsync(lesson);
			var allLessonAsync = await _lessonRepository.GetAllLessonAsync();
			return View("LessonTable", allLessonAsync);
		}

		[HttpPost]
		public async Task<IActionResult> UpdateLesson(int id, string title, string videoUrl, string information, int courseId)
		{
			var lesson = await _lessonRepository.GetLessonByIdAsync(id);
			lesson.Title = title;
			lesson.VideoUrl = videoUrl;
			lesson.Information = information;
			lesson.Course = await _courseRepository.Get(courseId);
			await _lessonRepository.UpdateLessonAsync(lesson);
			var task = await _lessonRepository.GetAllLessonAsync();
			return View("LessonTable", task);
		}


		public async Task<IActionResult> UpdateLesson()
		{
			return View("GetById");
		}

		[HttpPost]
		public async Task<IActionResult> GetLessonById(int id)
		{
			var lesson = await _lessonRepository.GetLessonByIdAsync(id);
			ViewBag.CourseList = await _courseRepository.GetAll();
			return View("UpdateLesson", lesson);
		}

		public async Task<IActionResult> DeleteLesson()
		{
			return View("DeleteLesson");
		}

		[HttpPost]
		public async Task<IActionResult> DeleteLesson(int id)
		{
			await _lessonRepository.DeleteLessonAsync(id);
			var lessons = await _lessonRepository.GetAllLessonAsync();
			return View("LessonTable", lessons);
		}

	}
}
