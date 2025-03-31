using CRUD_Project.Controllers;
using CRUD_Project.Utils;
using CRUD_Project.DTOs;
using CRUD_Project.Models;
using CRUD_Project.Enums;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace CRUD_Project.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly TaskDbContext _logger;

        public TaskRepository(TaskDbContext logger)
        {
            _logger = logger;
        }

        public async Task<TaskModel> GetTask(int id)
        {
            return await _logger.Tasks.Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<List<TaskModel>> GetAllTasks()
        {
            return await _logger.Tasks.ToListAsync();
        }

        public async Task<List<TaskModel>> GetCompletedTasks()
        {
            return await _logger.Tasks.Where(x => x.status == State.Finished).ToListAsync();
        }

        public async Task<TaskModel> AddTask(TaskCreateDTO task)
        {
            int newId = 0;
            if (_logger.Tasks.Any())
            {
                newId = _logger.Tasks.AsEnumerable().Max(t => t.Id) + 1;
            }
            var newTask = new TaskModel
            {
                Id = newId,
                title = task.title,
                status = State.Unfinished,
                dateCreated = DateTime.UtcNow.ToLocalTime(),
                dateLimit = task.dateLimit.ToUniversalTime().ToLocalTime()
            };

            await _logger.Tasks.AddAsync(newTask);
            await _logger.SaveChangesAsync();
            return newTask;
        }

        public async Task<TaskModel> UpdateTask(TaskUpdateDTO task, int id)
        {
            TaskModel taskToUpdate = await GetTask(id);
            if(taskToUpdate == null)
            {
                throw new Exception();
            }
            taskToUpdate.title = task.title;
            taskToUpdate.status = task.status;
            taskToUpdate.dateLimit = task.dateLimit;

            _logger.Tasks.Update(taskToUpdate);
            await _logger.SaveChangesAsync();
            return taskToUpdate;
        }

        public async Task<List<TaskModel>> DeleteTask(int id)
        {
            TaskModel taskToDelete = await GetTask(id);
            if (taskToDelete == null)
            {
                throw new Exception();
            }
            _logger.Tasks.Remove(taskToDelete);
            await _logger.SaveChangesAsync();
            return await _logger.Tasks.ToListAsync();
        }
    }
}
