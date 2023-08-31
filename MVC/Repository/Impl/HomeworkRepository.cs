using Microsoft.EntityFrameworkCore;
using MVC.Data;
using MVC.Entity;
using Task = System.Threading.Tasks.Task;

namespace MVC.Repository.Impl
{
	public class HomeworkRepository : IHomeworkRepository
	{
		private readonly AppDbContext _context;

		public HomeworkRepository(AppDbContext context) => _context = context;

		public async Task<List<Homework>> GetAllHomeworkAsync()
		{
			return await _context.Homework.ToListAsync();
		}

		public async Task<Homework> GetHomeworkByIdAsync(int id)
		{
			return await _context.Homework
				.FirstOrDefaultAsync(e => e.Id == id) ?? throw new BadHttpRequestException("Not Found");
		}

		public async Task AddHomeworkAsync(Homework homework)
		{
			_context.Homework.Add(homework);
			await _context.SaveChangesAsync();
		}

		public async Task DeleteHomeworkAsync(int id)
		{
			var homework = await _context.Homework.FindAsync(id);
			if (homework != null)
			{
				_context.Homework.Remove(homework);
				await _context.SaveChangesAsync();
			}
		}

		public async Task UpdateHomework(Homework homework)
		{
			_context.Entry(homework).State = EntityState.Modified;
			await _context.SaveChangesAsync();
		}
	}
}
