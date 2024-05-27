using TestTask.Data;
using TestTask.Models;
using TestTask.Services.Interfaces;

namespace TestTask.Services.Implementations
{
    public class OrderService : IOrderService
    {
        private readonly ApplicationDbContext _context;

        public OrderService(ApplicationDbContext context)
        {
            _context = context;
        }

        public Task<Order> GetOrder()
        {
            return Task.FromResult(_context.Orders.AsEnumerable()
                                                  .Where(o => o.Quantity > 1)
                                                  .MaxBy(o => o.CreatedAt));
        }

        public Task<List<Order>> GetOrders()
        {
            return Task.FromResult(_context.Orders.Where(o => o.User.Status == Enums.UserStatus.Active)
                                                  .OrderBy(o => o.CreatedAt)
                                                  .ToList());
        }
    }
}
