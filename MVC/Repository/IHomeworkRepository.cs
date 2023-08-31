using MVC.Dto;
using System.Security.Claims;
using MVC.Entity;
using Task = System.Threading.Tasks.Task;

namespace MVC.Repository
{
	public interface IHomeworkRepository
	{
		Task<List<Homework>> GetAllHomeworkAsync();
		Task<Homework> GetHomeworkByIdAsync(int id);
		Task AddHomeworkAsync(Homework homework);
		Task DeleteHomeworkAsync(int id);
		Task UpdateHomework(Homework homework);
	}
}
