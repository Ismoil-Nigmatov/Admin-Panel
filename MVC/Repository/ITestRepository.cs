using MVC.Dto;
using System.Security.Claims;
using MVC.Entity;
using Task = System.Threading.Tasks.Task;

namespace MVC.Repository
{
	public interface ITestRepository
	{
		Task<List<Test>> GetAll();
		Task<Test> GetTestById(int id);
		Task AddTestAsync(Test test);
		Task DeleteTestAsync(int id);
		Task UpdateTest (Test test);
	}
}
