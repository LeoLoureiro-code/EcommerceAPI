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
    public class OrderDetailRepository
    {
        private readonly EcommerceDbContext _context;

        public OrderDetailRepository(EcommerceDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Orderdetail>> GetAllOrderDetailsAsync()
        {
            return await _context.Orderdetails
                                 .Include(o => o.Product)
                                 .Include(o => o.Order)
                                 .ToListAsync();
        }

        public async Task<Orderdetail?> GetOrderDetailByIdAsync(int id)
        {
            return await _context.Orderdetails
                                 .Include(o => o.Product)
                                 .Include(o => o.Order)
                                 .FirstOrDefaultAsync(o => o.OrderDetailId == id);
        }

        public async Task AddOrderDetailAsync(Orderdetail orderDetail)
        {
            await _context.Orderdetails.AddAsync(orderDetail);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateOrderDetailAsync(Orderdetail orderDetail)
        {
            _context.Orderdetails.Update(orderDetail);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteOrderDetailAsync(int id)
        {
            var detail = await _context.Orderdetails.FindAsync(id);
            if (detail != null)
            {
                _context.Orderdetails.Remove(detail);
                await _context.SaveChangesAsync();
            }
        }
    }
}
