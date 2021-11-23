using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskApp.Models;
using TaskApp.SeedWork;

namespace TaskApp
{
    public class EmployeeRepository: IRepository<Employee>
    {
        private readonly ApplicationDbContext _db;

        public EmployeeRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public Employee Find(string Id)
        {
            return _db.Employee.Find(Id);
        }

        public List<Employee> List()
        {
            return _db.Employee.ToList();
        }

        public void Add(Employee model)
        {
            throw new NotImplementedException();
        }

        public void Delete(string Id)
        {
            throw new NotImplementedException();
        }

        public void Update(Employee model)
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
