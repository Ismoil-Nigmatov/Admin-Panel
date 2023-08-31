using Microsoft.EntityFrameworkCore;
using MVC.Data;
using MVC.Entity;
using Task = System.Threading.Tasks.Task;

namespace MVC.Repository.Impl
{
	public class LessonRepository : ILessonRepository
	{

		private readonly AppDbContext _context;

		public LessonRepository(AppDbContext context) => _context = context;

		public async Task<List<Lesson>> GetAllLessonAsync()
		{

			var lessons =  _context.Lesson.Include(e => e.Course);
			return lessons.ToList();
		}

		public async Task<Lesson> GetLessonByIdAsync(int id)
		{
			return await _context.Lesson
				.Include(e => e.Course)
				.FirstOrDefaultAsync(e => e.Id == id) ?? throw new BadHttpRequestException("Not Found");
		}

		public async Task AddLessonAsync(Lesson lessonDto)
		{
			_context.Lesson.Add(lessonDto);
			await _context.SaveChangesAsync();
		}

		public async Task UpdateLessonAsync(Lesson lessonDto)
		{
			_context.Entry(lessonDto).State = EntityState.Modified;
			await _context.SaveChangesAsync();
		}

		public async Task DeleteLessonAsync(int id)
		{
			var lesson = await _context.Lesson.FindAsync(id);
			if (lesson != null)
			{
				_context.Lesson.Remove(lesson);
				await _context.SaveChangesAsync();
			}
		}
	}
}
