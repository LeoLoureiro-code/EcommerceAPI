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
    public class CartItemRepository 
    {
        private readonly EcommerceDbContext _context;

        public CartItemRepository(EcommerceDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Cartitem>> GetAllCartItemsAsync()
        {
            return await _context.Cartitems
                                 .Include(c => c.Product)
                                 .Include(c => c.User)
                                 .ToListAsync();
        }

        public async Task<Cartitem?> GetCartItemByIdAsync(int id)
        {
            return await _context.Cartitems
                                 .Include(c => c.Product)
                                 .Include(c => c.User)
                                 .FirstOrDefaultAsync(c => c.CartItemId == id);
        }

        public async Task AddCartItemAsync(Cartitem cartItem)
        {
            await _context.Cartitems.AddAsync(cartItem);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateCartItemAsync(Cartitem cartItem)
        {
            _context.Cartitems.Update(cartItem);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteCartItemAsync(int id)
        {
            var cartItem = await _context.Cartitems.FindAsync(id);
            if (cartItem != null)
            {
                _context.Cartitems.Remove(cartItem);
                await _context.SaveChangesAsync();
            }
        }
    }
}
