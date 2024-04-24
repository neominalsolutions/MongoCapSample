using DotNetCore.CAP;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoCapSample.Models;
using MongoDB.Driver;

namespace MongoCapSample.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class PublishsController : ControllerBase
  {
    private readonly ICapPublisher _capBus;
    private readonly IMongoClient mongoClient;

    public PublishsController(ICapPublisher _capBus, IMongoClient mongoClient)
    {
      this._capBus = _capBus;
      this.mongoClient = mongoClient;
      
      
    }

    [HttpPost]
    public async Task<IActionResult> Publish()
    {
      using (var session = mongoClient.StartTransaction(_capBus, true))
      {
        
          var product = new ProductEvent
          {
            Name = "Product-1",
            Price = 10,
            Stock = 20
          };

          await this._capBus.PublishAsync("product-event", product);
        
      }

      return Ok();
    }
  }
}
