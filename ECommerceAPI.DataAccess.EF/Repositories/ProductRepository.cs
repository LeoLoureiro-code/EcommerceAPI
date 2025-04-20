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
    public class ProductRepository
    {

        private readonly EcommerceDbContext _context;

        public ProductRepository(EcommerceDbContext context) 
        { 
            _context = context;
        }

        public async Task<Product> GetProductByIdAsync(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
                throw new Exception("Product not found");

            return product;
        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            return await _context.Products.ToListAsync();
        }

        public async Task AddProduct (Product product)
        {
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateProduct(int productId, int productCategory, string productName, string productDescription, decimal productPrice, int stockQuantity, string imageUrl)
        {
            Product? existingProduct = await _context.Products.FindAsync(productId);

            if (existingProduct == null)
            {
                throw new Exception("Product not found");
            }

            existingProduct.CategoryId = productCategory;
            existingProduct.ProductName = productName;
            existingProduct.Description = productDescription;
            existingProduct.Price = productPrice;
            existingProduct.StockQuantity = stockQuantity;
            existingProduct.ImageUrl = imageUrl;

            await _context.SaveChangesAsync();

        }

        public async Task DeleteProduct(int id)
        {
            Product? product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                throw new Exception("Product not found");
            }
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
        }

    }
}
