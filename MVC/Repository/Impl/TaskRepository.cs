using Microsoft.EntityFrameworkCore;
using MVC.Data;
using MVC.Entity;
using Task = MVC.Entity.Task;

namespace MVC.Repository.Impl
{
	public class TaskRepository : ITaskRepository
	{

		private readonly AppDbContext _context;

		public TaskRepository(AppDbContext context) => _context = context;

		public async Task<List<Task>> GetAll()
		{
			var tasks = _context.Task.Include(e => e.Lesson);
			return tasks.ToList();
		}

		public async Task<Task> Get(int id)
		{
			return await _context.Task
				.Include(e => e.Lesson)
				.FirstOrDefaultAsync(e => e.Id == id) ?? throw new BadHttpRequestException("Not Found");
		}

		public async System.Threading.Tasks.Task Add(Task task)
		{
			_context.Task.Add(task);
			await _context.SaveChangesAsync();
		}

		public async System.Threading.Tasks.Task Update(Task task)
		{
			_context.Entry(task).State = EntityState.Modified;
			await _context.SaveChangesAsync();
		}

		public async System.Threading.Tasks.Task Delete(int id)
		{
			var task = await _context.Task.FindAsync(id);
			if (task != null)
			{
				_context.Task.Remove(task);
				await _context.SaveChangesAsync();
			}
		}
	}
}
