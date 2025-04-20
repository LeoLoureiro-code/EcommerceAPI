using ECommerceAPI.DataAccess.EF.Context;
using ECommerceAPI.DataAccess.EF.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.DataAccess.EF.Repositories
{
    public class UserRepository
    {
        private readonly EcommerceDbContext _context;

       public UserRepository(EcommerceDbContext context) 
        { 
            _context = context;
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
                throw new Exception("User not found");

            return user;
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User> CreateUser(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task UpdateUser(int userId, string firstName, string lastName, string email, string passwordHashed)
        {
            User? existingUser = await _context.Users.FindAsync(userId);

            if (existingUser == null)
            {
                throw new Exception("User not found");
            }

            existingUser.FirstName = firstName;
            existingUser.LastName = lastName;
            existingUser.Email = email;
            existingUser.PasswordHash = passwordHashed;

            await _context.SaveChangesAsync();

        }

        public async Task DeleteUser(int id)
        {
            User? user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                throw new Exception("User not found");
            }
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }

    }
}
