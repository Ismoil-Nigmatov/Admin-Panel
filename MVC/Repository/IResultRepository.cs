using System.Security.Claims;
using MVC.Dto;
using MVC.Entity;
using Task = System.Threading.Tasks.Task;

namespace MVC.Repository
{
	public interface IResultRepository
	{
		Task<List<Result>> GetAllResultAsync();
		Task<Result> GetResultByIdAsync(int id);
		Task AddResultAsync(Result result);
		Task DeleteResultAsync(int id);
		Task UpdateResultAsync(Result result);
		Task<List<ResultDTO>> GetUserResult(int userId);
	}
}
