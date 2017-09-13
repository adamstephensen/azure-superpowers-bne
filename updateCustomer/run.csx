#load "../shared/model/customer.csx"
using System.Net;

public static async Task<Customer> Run(HttpRequestMessage req, TraceWriter log, 
    IAsyncCollector<Customer> topicQueue)
{    
    Customer data = await req.Content.ReadAsAsync<Customer>();
    log.Info($"updateCustomer. Customer: {data.Id}");
    topicQueue.AddAsync(data);
    return data;
}
