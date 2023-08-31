using Microsoft.AspNetCore.Mvc;
using MVC.Entity;
using MVC.Entity.ENUMS;
using MVC.Repository;
using Task = MVC.Entity.Task;

namespace MVC.Controllers
{
	public class TaskController : Controller
	{
		private readonly ILessonRepository _lessonRepository;
		private readonly ITaskRepository _taskRepository;

		public TaskController(ILessonRepository lessonRepository, ITaskRepository taskRepository)
		{
			_lessonRepository = lessonRepository;
			_taskRepository = taskRepository;
		}
		public async Task<IActionResult> GetTaskList()
		{
			var allTasks = await _taskRepository.GetAll();

			return View("TaskTable", allTasks);
		}
		public async Task<IActionResult> AddTask()
		{
			ViewBag.Lessons = await _lessonRepository.GetAllLessonAsync();
			return View("AddTask");
		}
		[HttpPost]
		public async Task<IActionResult> AddTask(int lessonId, string title,string description)
		{
			Task task = new Task();
			task.Title = title;
			task.Description = description;
			task.Lesson = await _lessonRepository.GetLessonByIdAsync(lessonId);
			await _taskRepository.Add(task);
			var tasks = await _taskRepository.GetAll();
			return View("TaskTable", tasks);
		}
		[HttpPost]
		public async Task<IActionResult> GetTaskById(int id)
		{
			var task = await _taskRepository.Get(id);
			ViewBag.Lessons = await _lessonRepository.GetAllLessonAsync();
			return View("UpdateTask", task);
		}
		[HttpPost]
		public async Task<IActionResult> UpdateTask( int id , int lessonId, string title, string description)
		{
			var task = await _taskRepository.Get(id);
			task.Title = title;
			task.Description = description;
			task.Lesson = await _lessonRepository.GetLessonByIdAsync(lessonId);
			await _taskRepository.Update(task);
			var all = await _taskRepository.GetAll();
			return View("TaskTable", all);
		}
		public async Task<IActionResult> UpdateTask()
		{
			return View("GetById");
		}
		public async Task<IActionResult> DeleteTask()
		{
			return View("DeleteTask");
		}
		[HttpPost]
		public async Task<IActionResult> DeleteTask(int id)
		{
			await _taskRepository.Delete(id);
			var educations = await _taskRepository.GetAll();
			return View("TaskTable", educations);
		}
	}
}
