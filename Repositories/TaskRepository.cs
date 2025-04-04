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
        private readonly TaskSystemDbContext _context;

        public TaskRepository(TaskSystemDbContext context)
        {
            _context = context;
        }

        public async Task<TaskModel?> GetTask(int id)
        {
            return await _context.Tasks
                .Include(x => x.user)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<TaskModel>> GetAllTasks()
        {
            return await _context.Tasks
                .Include(x => x.user)
                .ToListAsync();
        }

        public async Task<List<TaskModel>> GetCompletedTasks()
        {
            return await _context.Tasks
                .Include(x => x.user)
                .Where(x => x.status == State.Finished)
                .ToListAsync();
        }

        public async Task<TaskModel> AddTask(TaskCreateDTO task)
        {
            int newId = 0;
            if (_context.Tasks.Any())
            {
                newId = _context.Tasks.AsEnumerable().Max(t => t.Id) + 1;
            }

            var newTask = new TaskModel
            {
                Id = newId,
                title = task.title,
                status = State.Unfinished,
                dateCreated = DateTime.UtcNow,
                dateLimit = task.dateLimit.ToUniversalTime(),
                userId = task.userId
            };

            var result = await _context.Tasks.AddAsync(newTask);
            await _context.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<TaskModel> UpdateTask(TaskModel oldTask, TaskUpdateDTO task)
        {
            oldTask.title = task.title;
            oldTask.status = task.status;
            oldTask.dateLimit = task.dateLimit;
            oldTask.userId = task.userId;

            var result = _context.Tasks.Update(oldTask);
            await _context.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<bool> DeleteTask(TaskModel task)
        {
            var result = _context.Tasks.Remove(task);
            await _context.SaveChangesAsync();
            return result.State == EntityState.Detached;
        }
    }
}
