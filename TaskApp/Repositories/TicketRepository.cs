using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskApp.Models;
using TaskApp.SeedWork;

namespace TaskApp.Repositories
{
    public class TicketRepository:IRepository<Ticket>
    {
        private readonly ApplicationDbContext _db;

        public TicketRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public Ticket Find(string Id)
        {
            return _db.Ticket.Find(Id);
        }

        public List<Ticket> List()
        {
            return _db.Ticket.ToList();
        }

        public void Add(Ticket ticket)
        {
            _db.Ticket.Add(ticket);
            _db.SaveChanges();

        }

        public void Delete(string Id)
        {
            throw new NotImplementedException();
        }

        public void Update(Ticket model)
        {
            _db.Ticket.Update(model);
            _db.SaveChanges();
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
