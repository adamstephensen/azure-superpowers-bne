#load "../shared/model/customer.csx"

using System.Net;
using System.Configuration;
using System.Data.SqlClient;
using Dapper;

public static dynamic Run(HttpRequestMessage req, string id,
TraceWriter log)
{
    var connection = ConfigurationManager.ConnectionStrings["SqlConnection"].ConnectionString;
    log.Info("C# HTTP trigger function processed a request.");

    using (var db = new SqlConnection(connection))
    {
        if (string.IsNullOrEmpty(id))
        {
            List<Customer> customers = db.Query<Customer>("SELECT * FROM [dbo].[Customers]").ToList();
            return customers;
        }
        else
        {
            Customer customer = db.Query<Customer>("SELECT * FROM [dbo].[Customers] WHERE CustomerId = @Id", new { Id = id }).FirstOrDefault();
            return customer;
        }
    }
}
