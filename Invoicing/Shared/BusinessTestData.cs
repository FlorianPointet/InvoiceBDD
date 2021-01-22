using System;
using System.Collections.Generic;
using System.Linq;

namespace Invoicing.Shared
{
    public class BusinessTestData : IBusinessData
    {
        private Invoice[] testInvoices =
        {
            new Invoice("BSO349", "FOO", 2123.4m     , DateTime.Now),
            new Invoice("BSO345", "BAR", 5000.45m    , DateTime.Now),
            new Invoice("BSO450", "BAR", 123.4m      , DateTime.Now),
            new Invoice("BSO410", "BOO", 1000.12m   , DateTime.Now)
        };

        public BusinessTestData()
        {
            testInvoices[1].RegisterPayment(DateTime.Now, 5000.45m);
            testInvoices[3].RegisterPayment(DateTime.Now, 500.46m);
            testInvoices[3].Expected = DateTime.Now;
        }
        
        public IEnumerable<Invoice> AllInvoices => testInvoices;

        public decimal SalesRevenue => testInvoices.Sum(invoice => invoice.Amount);

        public decimal Outstanding => testInvoices.Sum(invoice => invoice.Amount - invoice.Paid);
    }
}
