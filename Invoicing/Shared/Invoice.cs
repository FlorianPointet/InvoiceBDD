using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invoicing.Shared
{
    public class Invoice
    {
        public Invoice(string reference, string customer, decimal amount, DateTime created) 
        {
            Reference = reference;
            Customer = customer;
            Amount = amount;
            Created = created;
            Expected = created + TimeSpan.FromDays(30);
        }
        [Required(ErrorMessage ="Invoice reference is required")]
        public string Reference { get; }
        public string Customer { get; }
        public decimal Amount { get; }
        public decimal Paid { get; private set; } = 0m;
        public DateTime Created { get; }
        public DateTime Expected { get; set; }
        public DateTime? LastPayment { get; private set; } = null;

        public bool IsPaid => Paid == Amount;
        public bool IsLate => Expected < DateTime.Now;

        public void RegisterPayment(DateTime when, decimal howMany)
        {
            if(when < Created)
            {
                throw new ArgumentOutOfRangeException("Cannot pay before the invoice creation");
            }
            LastPayment = when;
            if(Amount-Paid < howMany)
            {
                throw new ArgumentOutOfRangeException("Cannot pay over the due amount");
            }
            Paid += howMany;
        }
    }
}
