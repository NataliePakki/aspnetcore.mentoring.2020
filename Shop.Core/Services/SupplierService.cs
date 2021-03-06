﻿using System.Collections.Generic;
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

        public void Delete(int id)
        {
        }

        public Supplier Get(int id, bool includeAll = false)
        {
            return _context.Suppliers.FirstOrDefault(x => x.SupplierID == id);
        }

        public IEnumerable<Supplier> GetAll(bool includeAll = false)
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
