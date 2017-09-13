#load "../shared/model/customer.csx"

using System;
using System.Configuration;
using System.Threading.Tasks;
using ServiceStack.Redis;
using ServiceStack.Text;

public static void Run(Customer customer, TraceWriter log)
{
    log.Info($"cacheCustomer customer:{customer}");
    var redisString  = ConfigurationManager.ConnectionStrings["MyRedis"].ConnectionString;
    var redisManager = new RedisManagerPool(redisString);
    var redis = redisManager.GetClient();
    var redisCustomer = redis.As<Customer>();
    redisCustomer.Store(customer);
}
