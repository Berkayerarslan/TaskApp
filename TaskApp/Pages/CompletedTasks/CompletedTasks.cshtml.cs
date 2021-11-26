using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TaskApp.Models;
using TaskApp.Repositories;

namespace TaskApp.Pages.CompletedTasks
{
    public class CompletedTasksModel : PageModel
    {

        private readonly CustomerRepository _customerRepository;
        private readonly EmployeeRepository _employeeRepository;
        private readonly TicketRepository _ticketRepository;

        public CompletedTasksModel(CustomerRepository customerRepository, EmployeeRepository employeeRepository, TicketRepository ticketRepository)
        {
            _customerRepository = customerRepository;
            _employeeRepository = employeeRepository;
            _ticketRepository = ticketRepository;

        }
        public List<Ticket> Tickets { get; set; }
        public void OnGet()
        {
            Tickets = _ticketRepository.List().FindAll(x => x.Status == TicketStatus.Completed);
        }
    }
}
