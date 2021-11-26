using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TaskApp.Models;
using TaskApp.Repositories;
using TaskApp.Services;

namespace TaskApp.Pages.ClosedTasks
{
    public class CompleteOrReviewModel : PageModel
    {
        private readonly CustomerRepository _customerRepository;
        private readonly EmployeeRepository _employeeRepository;
        private readonly TicketRepository _ticketRepository;
        private readonly TicketService _ticketServise;

        public CompleteOrReviewModel(CustomerRepository customerRepository, EmployeeRepository employeeRepository, TicketRepository ticketRepository, TicketService ticketService)
        {
            _customerRepository = customerRepository;
            _employeeRepository = employeeRepository;
            _ticketRepository = ticketRepository;
            _ticketServise = ticketService;

        }
        [BindProperty]
        public List<Ticket> Tickets { get; set; }
        [BindProperty]
        public List<Employee> Employees { get; set; } = new();
        public void OnGet()
        {
            Tickets = _ticketRepository.List().FindAll(x => x.Status == TicketStatus.Closed);
            foreach (var item in Tickets)
            {
                Employees.Add(_employeeRepository.Find(item.EmployeeId));
            }
        }
        public void OnPostReview(string id)
        {
            if (ModelState.IsValid)
            {
                var ticket = _ticketRepository.Find(id);
                _ticketServise.ReviewTicket(ticket);
            }
            OnGet();
        }

        public void OnPostComplete(string id)
        {
            if (ModelState.IsValid)
            {
                var ticket = _ticketRepository.Find(id);
                _ticketServise.CompleteTicket(ticket);
            }
            OnGet();
        }

    }
}
