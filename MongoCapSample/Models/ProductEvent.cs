namespace MongoCapSample.Models
{
  public class ProductEvent
  {
    public string Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public int Stock { get; set; }

    public ProductEvent()
    {
      Id = Guid.NewGuid().ToString();
    }

  }
}
