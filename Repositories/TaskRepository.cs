using CRUD_Project.Controllers;
using CRUD_Project.Utils;
using CRUD_Project.DTOs;
using CRUD_Project.Models;

namespace CRUD_Project.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly TaskContext _logger;

        public TaskRepository(TaskContext logger)
        {
            _logger = logger;
        }

        public Task<List<TaskModel>> GetTask(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<TaskModel>> GetAllTasks()
        {
            throw new NotImplementedException();
        }

        public Task<List<TaskModel>> GetCompletedTasks()
        {
            throw new NotImplementedException();
        }

        public Task<List<TaskModel>> AddTask(TaskDTO task)
        {
            throw new NotImplementedException();
        }

        public Task<List<TaskModel>> UpdateTask(TaskUpdateDTO task, int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<TaskModel>> DeleteTask(int id)
        {
            throw new NotImplementedException();
        }
    }
}
