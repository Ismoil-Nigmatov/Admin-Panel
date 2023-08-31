using MVC.Entity;
using Task = System.Threading.Tasks.Task;

namespace MVC.Repository
{
	public interface IContactRepository
	{
		Task<List<Contact>> GetAll();
		Task Delete(int id);
	}
}
