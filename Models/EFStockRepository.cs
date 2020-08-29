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

        public void RemoveItem(SparePart sparePart)
        {
            context.SpareParts.Remove(sparePart);
            context.SaveChanges();
        }

        public void UpdateItem(SparePart sparePart)
        {
            context.SpareParts.Update(sparePart);
            context.SaveChanges();
        }
    }
}
