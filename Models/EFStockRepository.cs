using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TechnicalStock.Models
{
    public class EFStockRepository : IStockRepository
    {
        private StockDBContext context;
        public EFStockRepository(StockDBContext ctx)
        {
            context = ctx;
        }

        public IQueryable<SparePart> SpareParts => context.SpareParts;
        public void AddNewItem(SparePart newPart)
        {
            context.SpareParts.Add(newPart);
            context.SaveChanges();
        }
    }
}
