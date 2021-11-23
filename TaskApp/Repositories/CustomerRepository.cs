using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskApp.Models;
using TaskApp.SeedWork;

namespace TaskApp
{
    public class CustomerRepository : IRepository<Customer>
    {
        private readonly ApplicationDbContext _db;

        public CustomerRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public Customer Find(string Id)
        {
            return _db.Customer.Find(Id);
        }

        public List<Customer> List()
        {
            return _db.Customer.ToList();
        }

        public void Add(Customer model)
        {
            throw new NotImplementedException();
        }

        public void Delete(string Id)
        {
            throw new NotImplementedException();
        }

        public void Update(Customer model)
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            throw new NotImplementedException();
        }

        public bool SaveResult()
        {
            throw new NotImplementedException();
        }
    }
}
