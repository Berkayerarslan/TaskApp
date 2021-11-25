using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TaskApp.Models;
using TaskApp.Repositories;
using TaskApp.Services;

namespace TaskApp.Pages.TicketTech
{
    public class SetTechDetailModel : PageModel
    {
        private readonly TicketRepository _ticketRepository;
        private readonly CustomerRepository _customerRepository;
        private readonly TicketService _ticketService;

        public SetTechDetailModel(TicketRepository ticketRepository,CustomerRepository customerRepository,TicketService ticketService)
        {
            _ticketRepository = ticketRepository;
            _customerRepository = customerRepository;
            _ticketService = ticketService;
        }
        [BindProperty]
        public Ticket TicketInput { get; set; }
        [BindProperty]
        public Customer Customer { get; set; }

        [BindProperty]
        public string CustomerName { get; set; }

        //public List<short> priorties = new List<short>();

        [BindProperty,Required(ErrorMessage ="Seçim Yapýlmalý")]
        public string Priorty { get; set; }
        public string[] priorties = {"Çok Kolay","Kolay","Orta Zorlukta","Zor","Çok Zor"};

        [BindProperty,Required(ErrorMessage ="Seçim Yapýlmalý")]
        public short Rank { get; set; }
        public short[] ranks = { 1, 2, 3, 4, 5 };

        public void OnGet(string id)
        {
            TicketInput = _ticketRepository.Find(id);
            Customer = _customerRepository.Find(_ticketRepository.Find(id).CustomerId);
            CustomerName = Customer.Name;
        }

        public void OnPostSave(string id)
        {
            if (ModelState.IsValid)
            {
                TicketInput = _ticketRepository.Find(id);
                Customer = _customerRepository.Find(_ticketRepository.Find(id).CustomerId);
                _ticketService.UpdateTicket(TicketInput,Priorty,Rank);
            }

           
            
        }
    }
}
