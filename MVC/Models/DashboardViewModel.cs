using MVC.Dto;
using MVC.Entity;

namespace MVC.Models
{
	public class DashboardViewModel
	{
		public User CurrentUser { get; set; }
		public List<User> AllUsers { get; set; }
	}
}
