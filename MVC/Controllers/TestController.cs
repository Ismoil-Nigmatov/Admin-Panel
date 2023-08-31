using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using MVC.Entity;
using MVC.Repository;
using MVC.Repository.Impl;

namespace MVC.Controllers
{
	public class TestController : Controller
	{
		private readonly ITestRepository _testRepository;

		public TestController(ITestRepository testRepository)
		{
			_testRepository = testRepository;
		}

		public async Task<IActionResult> GetTests()
		{
			var allTests = await _testRepository.GetAll();

			return View("TestTable", allTests);
		}

		public async Task<IActionResult> AddTest()
		{

			return View("AddTest");
		}

		[HttpPost]
		public async Task<IActionResult> AddTest(string question, List<string> options, string right)
		{
			Test test = new Test();
			test.Question = question;
			test.Options = options;
			test.RightOption = right;
			await _testRepository.AddTestAsync(test);
			var tests = await _testRepository.GetAll();
			return View("TestTable", tests);
		}


		public async Task<IActionResult> GetTestById()
		{

			return View("GetTest");
		}

		[HttpPost]
		public async Task<IActionResult> GetTestById(int id)
		{
			var test = await _testRepository.GetTestById(id);

			return View("UpdateTest", test);
		}

		[HttpPost]
		public async Task<IActionResult> UpdateTest(int id, string question, List<string> options, string right)
		{
			var test = await _testRepository.GetTestById(id);
			test.Question = question;
			test.Options = options;
			test.RightOption = right;
			await _testRepository.UpdateTest(test);
			var tests = await _testRepository.GetAll();
			return View("TestTable", tests);
		}


		public async Task<IActionResult> DeleteTest()
		{

			return View("DeleteTest");
		}

		[HttpPost]
		public async Task<IActionResult> DeleteTest(int id)
		{
			await _testRepository.DeleteTestAsync(id);
			var all = await _testRepository.GetAll();
			return View("TestTable", all);
		}
	}
}
