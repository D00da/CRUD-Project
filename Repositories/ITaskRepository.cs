﻿using CRUD_Project.Models;
using CRUD_Project.DTOs;

namespace CRUD_Project.Repositories
{
    public interface ITaskRepository
    {
        Task<TaskModel?> GetTask(int id);
        Task<List<TaskModel>> GetAllTasks();
        Task<List<TaskModel>> GetCompletedTasks();
        Task<TaskModel> AddTask(TaskCreateDTO task);
        Task<TaskModel> UpdateTask(TaskModel oldTask, TaskUpdateDTO task);
        Task<bool> DeleteTask(TaskModel task);
    }
    
}
