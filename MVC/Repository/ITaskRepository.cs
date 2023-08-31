using MVC.Entity;
using Task = System.Threading.Tasks.Task;

namespace MVC.Repository
{
	public interface ITaskRepository
	{
		Task<List<Entity.Task>> GetAll();
		Task<Entity.Task> Get(int id);
		Task Add(Entity.Task task);
		Task Update(Entity.Task task);
		Task Delete(int id);
	}
}
