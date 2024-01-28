using BasketApi.Infrastructure.Repositories;
using BasketApi.Services;
using StackExchange.Redis;
using ProductApi;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services
    .AddScoped<IProductService, ProductService>()
    .AddTransient<IBasketRepository, ReditBasketRepository>()
    .AddGrpcClient<ProductGrpc.ProductGrpcClient>((services, options) =>
{
    options.Address = new Uri("https://localhost:5107");
});
builder.Services.AddSingleton(sp =>
{
    string connectionString = builder.Configuration["ConnectionString"] ?? string.Empty;

    return ConnectionMultiplexer.Connect(connectionString);
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
