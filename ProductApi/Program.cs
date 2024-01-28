using Microsoft.EntityFrameworkCore;
using ProductApi.Extensions;
using ProductApi.Grpc;
using ProductApi.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddGrpc();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddContext(builder.Configuration);
var app = builder.Build();

app.MigrateDbContext<ProductContext>((context, services) =>
{
    IWebHostEnvironment env = services.GetRequiredService<IWebHostEnvironment>();
    ILogger<ProductContextSeed> logger = services.GetRequiredService<ILogger<ProductContextSeed>>();

    new ProductContextSeed()
        .SeedAsync(context, env, logger)
        .Wait();
});
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.MapGrpcService<ProductService>();

app.Run();
