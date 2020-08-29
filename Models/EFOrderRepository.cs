using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TechnicalStock.Models
{
    public class EFOrderRepository : IOrderRepository
    {
        private StockDBContext context;
        public EFOrderRepository(StockDBContext ctx)
        {
            context = ctx;
        }
        public IQueryable<Order> Orders => context.Orders.Include(o => o.Lines).ThenInclude(l => l.SparePart);

        public void SaveOrder(Order order)
        {
            context.AttachRange(order.Lines.Select(l => l.SparePart));
            if (order.OrderID == 0)
            {   
                context.Orders.Add(order);
            }
            context.SaveChanges();
        }
    }
}
