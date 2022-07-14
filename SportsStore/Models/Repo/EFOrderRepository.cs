using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace SportsStore.Models.Repo
{
    public class EFOrderRepository : IOrderRepository
    {
        private StoreDbContext context;

        public EFOrderRepository(StoreDbContext ctx)
        {
            context = ctx;
        }

        public IQueryable<Order> Orders => context.Orders
            .Include(o => o.Lines)
            .ThenInclude(l => l.Product);

        public void SaveOrder(Order order)
        {
            context.AttachRange(order.Lines.Select(l => l.Product));
            if (order.ID == 0)
            {
                context.Orders.Add(order);
            }

            context.SaveChanges();
        }
    }

}
