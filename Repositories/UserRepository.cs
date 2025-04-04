using CRUD_Project.DTOs;
using CRUD_Project.Enums;
using CRUD_Project.Models;
using CRUD_Project.Utils;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace CRUD_Project.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly TaskSystemDbContext _context;

        public UserRepository(TaskSystemDbContext context)
        {
            _context = context;
        }

        public async Task<UserModel?> GetUser(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<List<UserModel>> GetAllUsers()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<List<TaskModel>> GetAllUserTasks(int id)
        {
            return await _context.Tasks
                .Include(x => x.user)
                .Where(x => x.userId == id)
                .ToListAsync();
        }

        public async Task<UserModel> AddUser(UserDTO user)
        {
            int newId = 0;
            if (_context.Users.Any())
            {
                newId = _context.Users.AsEnumerable().Max(t => t.Id) + 1;
            }

            var newUser = new UserModel
            {
                Id = newId,
                name = user.name
            };

            var result = await _context.Users.AddAsync(newUser);
            await _context.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<UserModel> UpdateUser(UserModel oldUser, UserDTO user)
        {
            oldUser.name = user.name;

            var result = _context.Users.Update(oldUser);
            await _context.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<bool> DeleteUser(UserModel user)
        {
            var result = _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return result.State == EntityState.Detached;
        }
    }
}
