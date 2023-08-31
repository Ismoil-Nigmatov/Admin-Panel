using Microsoft.AspNetCore.Mvc;
using MVC.Entity;
using MVC.Repository;

namespace MVC.Controllers
{
	public class HomeworkController : Controller
	{
		private readonly IHomeworkRepository _homeworkRepository;
		private readonly ITaskRepository _taskRepository;

		public HomeworkController(IHomeworkRepository homeworkRepository, ITaskRepository taskRepository)
		{
			_homeworkRepository = homeworkRepository;
			_taskRepository = taskRepository;
		}

		public async Task<IActionResult> GetHomeworks()
		{
			var all = await _homeworkRepository.GetAllHomeworkAsync();

			return View("HomeworkTable", all);
		}

		public async Task<IActionResult> AddHomework()
		{
			ViewBag.TaskList = await _taskRepository.GetAll();
			return View("AddHomework");
		}

		[HttpPost]
		public async Task<IActionResult> AddHomework(int taskId, string image, string description)
		{
			Homework homework = new Homework();
			homework.ImageUrl = image;
			homework.Description = description;
			homework.Task = await _taskRepository.Get(taskId);
			await _homeworkRepository.AddHomeworkAsync(homework);
			var all = await _homeworkRepository.GetAllHomeworkAsync();
			return View("HomeworkTable", all);
		}

		[HttpPost]
		public async Task<IActionResult> UpdateHomework(int id,int taskId, string image, string description)
		{
			var lesson = await _homeworkRepository.GetHomeworkByIdAsync(id);
			lesson.ImageUrl = image;
			lesson.Description = description;
			lesson.Task = await _taskRepository.Get(taskId);
			await _homeworkRepository.UpdateHomework(lesson);
			var all = await _homeworkRepository.GetAllHomeworkAsync();
			return View("HomeworkTable", all);
		}


		public async Task<IActionResult> UpdateHomework()
		{
			return View("GetById");
		}

		[HttpPost]
		public async Task<IActionResult> GetHomeworkById(int id)
		{
			var lesson = await _homeworkRepository.GetHomeworkByIdAsync(id);
			ViewBag.TaskList = await _taskRepository.GetAll();
			return View("UpdateHomework", lesson);
		}

		public async Task<IActionResult> DeleteHomework()
		{
			return View("DeleteHomework");
		}

		[HttpPost]
		public async Task<IActionResult> DeleteHomework(int id)
		{
			await _homeworkRepository.DeleteHomeworkAsync(id);
			var homeworks = await _homeworkRepository.GetAllHomeworkAsync();
			return View("HomeworkTable", homeworks);
		}
	}
}
