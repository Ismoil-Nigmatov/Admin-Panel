using Microsoft.AspNetCore.Mvc;
using MVC.Entity;
using MVC.Repository;

namespace MVC.Controllers
{
	public class CourseController : Controller
    {
        private readonly ICourseRepository _courseRepository;

        public CourseController(ICourseRepository courseRepository)
        {
            _courseRepository = courseRepository;
        }

        public async Task<IActionResult> GetCourseList()
        {
            var allCourseAsync = await _courseRepository.GetAll();

            return View("_CourseCard", allCourseAsync);
        }

        public async Task<IActionResult> AddCourse()
        {

            return View("AddCourse");
        }

        [HttpPost]
        public async Task<IActionResult> AddCourse(string imageUrl , string description , string price)
        {
	        Course course = new Course();
            course.ImageUrl = imageUrl;
            course.Description = description;
            course.Price = Convert.ToDouble(price);
            await _courseRepository.Add(course);
            var task = await _courseRepository.GetAll();
            return View("_CourseCard", task);
        }

        public async Task<IActionResult> GetCourseById()
        {

	        return View("GetCourse");
        }

        [HttpPost]
        public async Task<IActionResult> GetCourseById(int id)
        {
	        var course = await _courseRepository.Get(id);

	        return View("UpdateCourse", course);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateCourse(int id, string image, string description, string price)
        {
	        var course = await _courseRepository.Get(id);
            course.ImageUrl = image;
            course.Description = description;
            course.Price = Convert.ToDouble(price);
            await _courseRepository.Update(course);
            var courses = await _courseRepository.GetAll();
            return View("_CourseCard", courses);
        }


        public async Task<IActionResult> DeleteCourse()
        {

	        return View("DeleteCourse");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteCourse(int id)
        {
	        await _courseRepository.Delete(id);
	        var all = await _courseRepository.GetAll();
	        return View("_CourseCard", all);
        }
	}
}
