using Dapper;
using Invoicing.Shared;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Invoicing.Server.Models
{
    public class BusinessDataSql : IBusinessData
    {
        private SqlConnection cnct;

        public BusinessDataSql(string connectionString)
        {
            cnct = new SqlConnection(connectionString);
        }

        public void Dispose()
        {
            cnct.Dispose();
        }
        public IEnumerable<Invoice> AllInvoices => throw new NotImplementedException();

        public decimal SalesRevenue => throw new NotImplementedException();

        public decimal Outstanding => throw new NotImplementedException();

        public void Add(Invoice newFacture)
        {
            var insert = @"INSERT INTO Factures(Rerence,Customer,Amount,Created,Paid,Expected) values (@Reference,@Customer,@Amount,@Created,@Paid,@Expected)";

            newFacture.Expected = newFacture.Created.AddMonths(1);

            cnct.Query(insert, newFacture);
        }

    }


}
