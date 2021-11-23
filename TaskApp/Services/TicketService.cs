using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskApp.Models;
using TaskApp.Repositories;

namespace TaskApp.Services
{
    public class TicketService
    {
        private TicketRepository _ticketRepository;
        private CustomerRepository _customerRepository;
        private NetSmptMailService _netSmptMailService;
        

        public TicketService(TicketRepository ticketRepository,NetSmptMailService netSmptMailService,CustomerRepository customerRepository)
        {
            _ticketRepository = ticketRepository;
            _netSmptMailService = netSmptMailService;
            _customerRepository = customerRepository;
        }

        public void AddTicket(Ticket ticket, string customerId)
        {
            if (ticket.Subject.Length > 50)
            {
                throw new Exception("Konu 50 karakterden fazla olamaz.");
            }

            if (ticket.Description.Length > 500)
            {
                throw new Exception("Açıklama 500 karakterden fazla olamaz.");
            }
            if (string.IsNullOrEmpty(ticket.Subject))
            {
                throw new Exception("Konu boş olarak gönderilemez.");
            }
            if (string.IsNullOrEmpty(ticket.Description))
            {
                throw new Exception("Açıklama boş olarak gönderilemez.");
            }
            
            
            ticket.CustomerId = customerId;
            _ticketRepository.Add(ticket);
            var customer = _customerRepository.Find(customerId);
            string message = $"{ticket.Subject} konulu ticket açılmıştır";
            _netSmptMailService.SendEmail(from: "nbuy.oglen@gmail.com", to: customer.Email, message: message,subject: ticket.Subject);
            


        }
    }
}
