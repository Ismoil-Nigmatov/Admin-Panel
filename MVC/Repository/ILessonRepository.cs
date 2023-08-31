using MVC.Entity;
using Task = System.Threading.Tasks.Task;

namespace MVC.Repository
{
	public interface ILessonRepository
	{

		Task<List<Lesson>> GetAllLessonAsync();
		Task<Lesson> GetLessonByIdAsync(int id);
		Task AddLessonAsync(Lesson lessonDto);
		Task UpdateLessonAsync(Lesson lessonDto);
		Task DeleteLessonAsync(int id);

	}
}
