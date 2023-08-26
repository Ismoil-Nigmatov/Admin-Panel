using MVC.Data;
using MVC.Entity;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using MVC.Dto;
using Task = System.Threading.Tasks.Task;

namespace MVC.Repository.Impl
{
	public class FeedbackRepository : IFeedbackRepository
	{
		private readonly AppDbContext _context;

		public FeedbackRepository(AppDbContext context) => _context = context;


		public async Task<List<FeedbackDTO>> GetAllFeedbackAsync()
		{
			var feedbackDtos = await _context.Feedback
				.Include(e => e.User)
				.Include(e => e.Education)
				.Select(e => new FeedbackDTO()
				{
					Id = e.Id,
					Description = e.Description,
					UserId = e.User.Id,
					EducationId = e.Education.Id
				})
				.ToListAsync();

			return feedbackDtos;
		}

		public async Task<FeedbackDTO> GetFeedbackByIdAsync(int id)
		{
			var firstOrDefaultAsync = await _context.Feedback
				.Include(e => e.User)
				.Include(e => e.Education)
				.FirstOrDefaultAsync(e => e.Id == id) ?? throw new BadHttpRequestException("Not Found");
			FeedbackDTO feedbackDto = new FeedbackDTO();
			feedbackDto.Id = id;
			feedbackDto.Description = firstOrDefaultAsync.Description;
			feedbackDto.UserId = firstOrDefaultAsync.User.Id;
			feedbackDto.EducationId = firstOrDefaultAsync.Education.Id;
			return feedbackDto;
		}

		public async Task AddFeedbackAsync(ClaimsPrincipal principal, FeedbackDTO feedbackDto)
		{
			var userId = principal.FindFirst(ClaimTypes.NameIdentifier)!.Value;

			Feedback feedback = new Feedback();
			feedback.Description = feedbackDto.Description;
			feedback.Education = await _context.Education.FindAsync(feedbackDto.EducationId) ?? throw new BadHttpRequestException("Education not found");
			feedback.User = await _context.User.FindAsync(Convert.ToInt32(userId)) ?? throw new BadHttpRequestException("User not found");
			_context.Feedback.Add(feedback);
			await _context.SaveChangesAsync();
		}

		public async Task DeleteFeedbackAsync(int id)
		{
			var feedback = await _context.Feedback.FindAsync(id);
			if (feedback != null)
			{
				_context.Feedback.Remove(feedback);
				await _context.SaveChangesAsync();
			}
		}

		public async Task<List<FeedbackDTO>> GetUserFeedbacks(int userId)
		{
			var feedbacksForUser = _context.Feedback
				.Where(feedback => feedback.User.Id == userId)
				.Include(feedback => feedback.Education) // Eagerly load the Education entity
				.Include(feedback => feedback.User) // Eagerly load the Education entity
				.Select(feedback => new FeedbackDTO
				{
					Id = feedback.Id,
					Description = feedback.Description,
					EducationId = feedback.Education.Id,
					UserId = feedback.User.Id
				})
				.ToList();
			return feedbacksForUser;
		}
	}
}
