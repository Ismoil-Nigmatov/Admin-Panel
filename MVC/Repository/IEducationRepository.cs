
using MVC.Entity;
using Task = System.Threading.Tasks.Task;

namespace MVC.Repository
{
	public interface IEducationRepository
	{
		Task<List<Education>> GetAll();
		Task<Education> Get(int id);
		Task Add(Education course);
		Task Update(Education course);
		Task Delete(int id);
	}
}
