using Microsoft.EntityFrameworkCore;
using MVC.Data;
using MVC.Entity;
using Task = System.Threading.Tasks.Task;

namespace MVC.Repository.Impl
{
	public class ContactRepository : IContactRepository
	{

		private readonly AppDbContext _context;

		public ContactRepository(AppDbContext context) => _context = context;

		public async Task<List<Contact>> GetAll()
		{
			return await _context.Contact.ToListAsync();
		}

		public async Task Delete(int id)
		{
			var contact = await _context.Contact.FindAsync(id);
			if (contact != null)
			{
				_context.Contact.Remove(contact);
				await _context.SaveChangesAsync();
			}
		}
	}
}
