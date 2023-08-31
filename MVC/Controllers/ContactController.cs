using Microsoft.AspNetCore.Mvc;
using MVC.Repository;

namespace MVC.Controllers
{
	public class ContactController : Controller
	{
		private readonly IContactRepository _contactRepository;

		public ContactController(IContactRepository contactRepository)
		{
			_contactRepository = contactRepository;
		}

		public async Task<IActionResult> GetContacts()
		{
			var allContacts = await _contactRepository.GetAll();

			return View("ContactTable", allContacts);
		}

		public async Task<IActionResult> DeleteContact()
		{
			return View("DeleteContact");
		}

		[HttpPost]
		public async Task<IActionResult> DeleteContact(int id)
		{
			await _contactRepository.Delete(id);
			var contacts = await _contactRepository.GetAll();
			return View("ContactTable", contacts);
		}
	}
}
