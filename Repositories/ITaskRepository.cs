using CRUD_Project.Models;
using CRUD_Project.DTOs;

namespace CRUD_Project.Repositories
{
    public interface ITaskRepository
    {
        Task<List<TaskModel>> GetTask(int id);
        Task<List<TaskModel>> GetAllTasks();
        Task<List<TaskModel>> GetCompletedTasks();
        Task<List<TaskModel>> AddTask(TaskDTO task);
        Task<List<TaskModel>> UpdateTask(TaskUpdateDTO task, int id);
        Task<List<TaskModel>> DeleteTask(int id);
    }
    
}
