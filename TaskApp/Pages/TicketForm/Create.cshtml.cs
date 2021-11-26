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

namespace TaskApp.Pages
{
    public class TicketFormModel : PageModel
    {
        private readonly CustomerRepository _customerRepository;
        private readonly EmployeeRepository _employeeRepository;
        private readonly TicketRepository _ticketRepository;
        private readonly TicketService _ticketService;

        public List<SelectListItem> SelectListItems = new List<SelectListItem>();

        [BindProperty]
        public string SelectedCustomerId { get; set; }
        public TicketFormModel(CustomerRepository customerRepository, EmployeeRepository employeeRepository, TicketRepository ticketRepository, TicketService ticketService)
        {
            _customerRepository = customerRepository;
            _employeeRepository = employeeRepository;
            _ticketRepository = ticketRepository;
            _ticketService = ticketService;
        }
        [BindProperty]
        public  Ticket TicketInput { get; set; }
        public void OnGet()
        {
            var Customers = _customerRepository.List();

            SelectListItems = Customers.Select(a =>
              new SelectListItem
              {
                  Value = a.Id,
                  Text = a.Name 
              }).ToList();
        }

        public void OnPostSave()
        {
            
            
            if (ModelState.IsValid)
            {
                _ticketService.AddTicket(TicketInput,SelectedCustomerId);
            }
            OnGet();
        }
    }
}
