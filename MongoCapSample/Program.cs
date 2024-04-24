using Microsoft.Extensions.Options;
using MongoCapSample.Subscriber;
using MongoDB.Driver;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<IMongoClient>(new MongoClient("mongodb://localhost:27017"));

builder.Services.AddScoped<ProductEventSubscriber>();

builder.Services.AddCap(x =>
{
  x.UseMongoDB(options =>
  {
    options.DatabaseName = "capDb";
    options.DatabaseConnection = "mongodb://localhost:27017";
  });
  x.UseRabbitMQ(options =>
  {
    options.ConnectionFactoryOptions = options =>
    {
      options.Ssl.Enabled = false;
      options.HostName = "localhost";
      options.UserName = "guest";
      options.Password = "guest";
      options.Port = 5672;
    };
  });

  x.UseDashboard(opt => { opt.PathMatch = "/cap-dashboard"; });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();


app.Run();
