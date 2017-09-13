#load "../shared/model/customer.csx"
using System.Net;
using Dapper;
using System.Data.SqlClient;
using System.Configuration;
using ServiceStack.Redis;
using ServiceStack.Text;
using System.Configuration;

public static void Run(TimerInfo myTimer, TraceWriter log)
{  
    log.Info($"C# Timer trigger function executed at: {DateTime.Now}");
    var cnnString  = ConfigurationManager.ConnectionStrings["SqlConnection"].ConnectionString;
    var redisString  = ConfigurationManager.ConnectionStrings["MyRedis"].ConnectionString;

    var redisManager = new RedisManagerPool(redisString);
    var redis = redisManager.GetClient();
    var redisCustomer = redis.As<Customer>();

    using(var db = new SqlConnection(cnnString))
    {
        var list = db.Query<Customer>("select * from [dbo].[Customers]").ToList();
        // foreach (var item in list)
        // {
        //     redisCustomer.Store(item);
        // }
        redisCustomer.StoreAll(list);
    }
}
