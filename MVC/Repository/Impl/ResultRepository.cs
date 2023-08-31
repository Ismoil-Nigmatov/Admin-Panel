using MVC.Data;
using MVC.Entity;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using MVC.Dto;
using Task = System.Threading.Tasks.Task;
using System.Threading.Tasks;

namespace MVC.Repository.Impl
{
	public class ResultRepository: IResultRepository
	{
		private readonly AppDbContext _context;

		public ResultRepository(AppDbContext context) => _context = context;


		public async Task<List<Result>> GetAllResultAsync()
		{
			var results = _context.Result.Include(e => e.User).Include(e => e.Education);
			return await results.ToListAsync();
		}

		public async Task<Result> GetResultByIdAsync(int id)
		{
			return await _context.Result
				.Include(e => e.User)
				.Include(e =>e.Education)
				.FirstOrDefaultAsync(e => e.Id == id) ?? throw new BadHttpRequestException("Not Found");
		}

		public async Task AddResultAsync(Result result)
		{
			_context.Result.Add(result);
			await _context.SaveChangesAsync();
		}

		public async Task DeleteResultAsync(int id)
		{
			var result = await _context.Result.FindAsync(id);
			if (result != null)
			{
				_context.Result.Remove(result);
				await _context.SaveChangesAsync();
			}
		}

		public async Task UpdateResultAsync(Result result)
		{
			_context.Entry(result).State = EntityState.Modified;
			await _context.SaveChangesAsync();
		}

		public async Task<List<ResultDTO>> GetUserResult(int userId)
		{
			var resultsForUser = _context.Result
				.Where(feedback => feedback.User.Id == userId)
				.Include(feedback => feedback.Education) // Eagerly load the Education entity
				.Include(feedback => feedback.User) // Eagerly load the Education entity
				.Select(feedback => new ResultDTO()
				{
					Id = feedback.Id,
					Url = feedback.Url,
					EducationId = feedback.Education.Id,
					UserId = feedback.User.Id
				})
				.ToList();
			return resultsForUser;
		}
	}
}
