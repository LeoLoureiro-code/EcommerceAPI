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
    public class CategoryRepository
    {
        private readonly EcommerceDbContext _context;

        public CategoryRepository(EcommerceDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Category>> GetAllCategoriesAsync()
        {
            return await _context.Categories
                .Include(c => c.Products)
                .ToListAsync();
        }

        public async Task<Category> GetCategoryByIdAsync(int id)
        {
            var category = await _context.Categories
                .Include(c => c.Products) 
                .FirstOrDefaultAsync(c => c.CategoryId == id);

            if (category == null)
                throw new Exception("Category not found");

            return category;
        }

        public async Task<Category> CreateCategory(Category category)
        {
            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();
            return category;
        }

        public async Task UpdateCategory(int categoryId, string name, string? description)
        {
            var existingCategory = await _context.Categories.FindAsync(categoryId);
            if (existingCategory == null)
                throw new Exception("Category not found");

            existingCategory.CategoryName = name;
            existingCategory.Description = description;

            await _context.SaveChangesAsync();
        }

        public async Task DeleteCategory(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null)
                throw new Exception("Category not found");

            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
        }
    }

}
