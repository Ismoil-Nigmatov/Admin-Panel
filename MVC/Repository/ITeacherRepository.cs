using MVC.Dto;
using System.Security.Claims;
using MVC.Entity;
using Task = System.Threading.Tasks.Task;

namespace MVC.Repository
{
	public interface ITeacherRepository
	{
		Task<List<Teacher>> GetAllTeacherAsync();
		Task<Teacher> GetTeacherByIdAsync(int id);
		Task AddTeacherAsync(Teacher teacher);
		Task UpdateTeacherAsync(Teacher teacher);
		Task DeleteTeacherAsync(int id);

	}
}
