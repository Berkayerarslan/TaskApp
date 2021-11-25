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
        /// <summary>
        /// Ticket oluşturduğumuz fonksiyon ve logic kısımları bulunuyor
        /// </summary>
        /// <param name="ticket">ekliceğim ticket</param>
        /// <param name="customerId">ticketa ait olan customerın Id sini alıyorum</param>
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
            
            // customer ıd atamasını yaptım.
            ticket.CustomerId = customerId;

            // ticket'a oluşma tarihini atadım.
            ticket.OpenTime = DateTime.Now;

            // ticket'a open status'ü ataması yaptım
            ticket.Status = TicketStatus.Open;

            // ticket repoya ticket ekliyorum
            _ticketRepository.Add(ticket);

            // mail atılıcak customerı çektim
            var customer = _customerRepository.Find(customerId);
            string message = $"{ticket.Subject} konulu ticket açılmıştır";

            // mail servisimiz çalıştı
            _netSmptMailService.SendEmail(from: "nbuy.oglen@gmail.com", to: customer.Email, message: message,subject: ticket.Subject);

        }


        public void UpdateTicket(Ticket ticket,string priorty,short rank)
        {
            if (rank == 0)
            {
                throw new Exception("Rank default 0 olarak gönderilemez.");
            }
            if (string.IsNullOrEmpty(priorty))
            {
                throw new Exception("Priorty boş gönderilemez.");
            }

            ticket.Priortiy = priorty;
            ticket.Rank = rank;
            _ticketRepository.Update(ticket);
        }

        public void TicketAddEmployee(Ticket ticket,string employeeId)
        {
            if (ticket.EmployeeId != null)
            {
                throw new Exception("Zaten Ticket bir çalışana atanmış durumda");
            }

            IsTicketCanBeAdded(ticket, employeeId);
            ticket.EmployeeId = employeeId;
            ticket.Status = TicketStatus.Assigned;
            ticket.AssignedTime = DateTime.Now;
            _ticketRepository.Update(ticket);
        }
        /// <summary>
        /// Çalışana iş ataması yaparken işin eklenebilir olup olmadığının kontrolü
        /// </summary>
        /// <param name="ticket">eklenecek ticket</param>
        /// <param name="employeeId">eklenicek employee</param>
        public void IsTicketCanBeAdded(Ticket ticket,string employeeId)
        {
            int total = 0;
            int rankCount = 0;
            var tickets = _ticketRepository.List();
            var hardTickets = tickets.FindAll(x => x.Priortiy == "Zor" && x.EmployeeId == employeeId);
            var rankTickets = tickets.FindAll(x => x.EmployeeId == employeeId && (x.Rank == 4 || x.Rank == 5));
            var myEmployeeTickets = tickets.FindAll(x => x.EmployeeId == employeeId);
            
            // Zor iş sayısı kontrolü yapılıyor

            if (hardTickets.Count > 3)
            {
                throw new Exception("Bir çalışana 3'ten fazla Zor iş eklenemez.");
            }

            // önem derecesi 4 5 veya olan işlerin çalışanın yapabileceğinden fazla mı kontrol edildi

            if (rankTickets.Count >= 4)
            {
                throw new Exception("Önem derecesi 4 5 olan 4 adet ticket çalışana eklenebilir.");
            }
            rankCount += rankTickets.Count;
            

            // Çalışanın işe ayıracağı vaktin yeni eklenicek ticketla beraber 160 saati aşıp aşmadığı kontrolü

            foreach (var item in myEmployeeTickets)
            {
                if (item.Priortiy == "Çok Kolay")
                {
                    total += 8;
                }
                else if (item.Priortiy == "Kolay")
                {
                    total += 16;
                }
                else if (item.Priortiy == "Orta Zorlukta")
                {
                    total += 24;
                }
                else if (item.Priortiy == "Zor")
                {
                    total += 32;
                }
                else if (item.Priortiy == "Çok Zor")
                {
                    total += 40;
                }
                else
                {
                    throw new Exception("Beklenmeyen bir değer geldi");
                }

            }
            if (ticket.Priortiy == "Çok Kolay")
            {
                total += 8;
            }
            else if (ticket.Priortiy == "Kolay")
            {
                total += 16;
            }
            else if (ticket.Priortiy == "Orta Zorlukta")
            {
                total += 24;
            }
            else if (ticket.Priortiy == "Zor")
            {
                total += 32;
            }
            else if (ticket.Priortiy == "Çok Zor")
            {
                total += 40;
            }

            if (total > 160)
            {
                throw new Exception("Toplam iş yükü 160 saati geçemez o yüzden bu iş ataması yapılamaz.");
            }

        }
    }
}
