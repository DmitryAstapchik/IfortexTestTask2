using TestTask.Data;
using TestTask.Models;
using TestTask.Services.Interfaces;

namespace TestTask.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _context;

        public UserService(ApplicationDbContext context)
        {
            _context = context;
        }

        public Task<User> GetUser()
        {
            return Task.FromResult(_context.Users.AsEnumerable()
                                                 .MaxBy(u => u.Orders?.Where(o => o.CreatedAt.Year == 2003 && o.Status == Enums.OrderStatus.Delivered)
                                                                      .Select(o => o.Price * o.Quantity)));
        }

        public Task<List<User>> GetUsers()
        {
            return Task.FromResult(_context.Users.Where(u => u.Orders.Any(o => o.CreatedAt.Year == 2010 && o.Status == Enums.OrderStatus.Paid))
                                                 .ToList());
        }
    }
}
