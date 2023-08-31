using Microsoft.EntityFrameworkCore;
using MVC.Data;
using MVC.Dto;
using MVC.Entity;
using Task = System.Threading.Tasks.Task;

namespace MVC.Repository.Impl
{
	public class EducationRepository : IEducationRepository
	{

		private readonly AppDbContext _context;

		public EducationRepository(AppDbContext context) => _context = context;

		public async Task<List<Education>> GetAll()
		{
			var educationsWithCourses = _context.Education.Include(e => e.Course);
			return educationsWithCourses.ToList();
		}

		public async Task<Education> Get(int id)
		{
			return await _context.Education
				.Include(e => e.Course)
				.FirstOrDefaultAsync(e => e.Id == id) ?? throw new BadHttpRequestException("Not Found");
		}

		public async Task Add(Education course)
		{
			_context.Education.Add(course);
			await _context.SaveChangesAsync();
		}

		public async Task Update(Education course)
		{
			_context.Entry(course).State = EntityState.Modified;
			await _context.SaveChangesAsync();
		}

		public async Task Delete(int id)
		{
			var education = await _context.Education.FindAsync(id);
			if (education != null)
			{
				_context.Education.Remove(education);
				await _context.SaveChangesAsync();
			}
		}
	}
}
