using System.Security.Claims;
using MVC.Dto;

namespace MVC.Repository
{
	public interface IResultRepository
	{
		Task<List<ResultDTO>> GetAllResultAsync();
		Task<ResultDTO> GetResultByIdAsync(int id);
		Task AddResultAsync(ClaimsPrincipal claims, ResultDTO resultDto);
		Task DeleteResultAsync(int id);

		Task<List<ResultDTO>> GetUserResult(int userId);
	}
}
