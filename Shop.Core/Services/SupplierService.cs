using System.Collections.Generic;
using System.Linq;

using Shop.Core.Data;
using Shop.Core.Models;

namespace Shop.Core.Services
{
    public class SupplierService: ISupplierService
    {
        private ApplicationContext _context;

        public SupplierService(ApplicationContext context)
        {
            _context = context;
        }

        public Supplier Create(Supplier data)
        {
            _context.Suppliers.Add(data);
            _context.SaveChanges();
            return data;
        }

        public Supplier Get(int id)
        {
            return _context.Suppliers.FirstOrDefault(x => x.SupplierID == id);
        }

        public IEnumerable<Supplier> GetAll()
        {
            return _context.Suppliers;
        }

        public Supplier Update(Supplier data)
        {
            _context.Update(data);
            _context.SaveChanges();
            return data;
        }
    }
}
