using System.Security.Claims;
using MVC.Dto;

namespace MVC.Repository
{
	public interface IFeedbackRepository
	{

		Task<List<FeedbackDTO>> GetAllFeedbackAsync();
		Task<FeedbackDTO> GetFeedbackByIdAsync(int id);
		Task AddFeedbackAsync(ClaimsPrincipal claims, FeedbackDTO feedbackDto);
		Task DeleteFeedbackAsync(int id);

		Task<List<FeedbackDTO>> GetUserFeedbacks(int userId);
	}
}
