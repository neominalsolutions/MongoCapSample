using DotNetCore.CAP;
using MongoCapSample.Models;

namespace MongoCapSample.Subscriber
{
  public class ProductEventSubscriber:ICapSubscribe
  {
    [CapSubscribe("product-event")]
    public async Task Consume(ProductEvent @event)
    {
      await Task.CompletedTask;
    }
  }
}
