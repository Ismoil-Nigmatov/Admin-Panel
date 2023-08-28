using Microsoft.EntityFrameworkCore;
using MVC.Data;
using MVC.Entity;
using Task = System.Threading.Tasks.Task;

namespace MVC.Repository.Impl
{
	public class TeacherRepository : ITeacherRepository
	{
		private readonly AppDbContext _context;

		public TeacherRepository(AppDbContext context) => _context = context;

		public Task<List<Teacher>> GetAllTeacherAsync()
		{
			return _context.Teacher.ToListAsync();
		}

		public Task<Teacher> GetTeacherByIdAsync(int id)
		{
			throw new NotImplementedException();
		}

		public Task AddTeacherAsync(Teacher teacher)
		{
			throw new NotImplementedException();
		}

		public Task UpdateTeacherAsync(Teacher teacher)
		{
			throw new NotImplementedException();
		}

		public Task DeleteTeacherAsync(int id)
		{
			throw new NotImplementedException();
		}
	}
}
