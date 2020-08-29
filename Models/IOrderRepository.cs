using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TechnicalStock.Models
{
    public interface IOrderRepository
    {
        IQueryable<Order> Orders { get; }
        public void SaveOrder(Order order);
    }
}
