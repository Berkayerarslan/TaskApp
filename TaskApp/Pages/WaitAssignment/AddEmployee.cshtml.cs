using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using TaskApp.Models;
using TaskApp.Repositories;
using TaskApp.Services;

namespace TaskApp.Pages.WaitAssignment
{
    public class AddEmployeeModel : PageModel
    {
        
        private readonly CustomerRepository _customerRepository;
        private readonly EmployeeRepository _employeeRepository;
        private readonly TicketRepository _ticketRepository;
        private readonly TicketService _ticketService;

        public AddEmployeeModel(CustomerRepository customerRepository, EmployeeRepository employeeRepository, TicketRepository ticketRepository, TicketService ticketService)
        {
             _customerRepository = customerRepository;
             _employeeRepository = employeeRepository;
             _ticketRepository = ticketRepository;
            _ticketService = ticketService;

        }

        public List<SelectListItem> SelectListItems = new List<SelectListItem>();

        [BindProperty]
        public string SelectedEmployeeId { get; set; }
        [BindProperty]
        public Ticket TicketInput { get; set; }
        [BindProperty]
        public List<Ticket> Tickets { get; set; }
        [BindProperty]
        public List<Employee> Employees { get; set; }

        public void OnGet()
        {

            Tickets = _ticketRepository.List().FindAll(x => x.Employee == null && x.Status == TicketStatus.Open && x.Priortiy != null && x.Rank != 0);
            
            var Employees = _employeeRepository.List();

            SelectListItems = Employees.Select(a =>
              new SelectListItem
              {
                  Value = a.Id,
                  Text = a.Name + a.Surname
              }).ToList();
        }
        public void OnPostSave(string id)
        {
           
            _ticketService.TicketAddEmployee(_ticketRepository.Find(id),SelectedEmployeeId);
            
        }
        
    }
}
