using DebeziumPoc.Api.Application.Common.Interfaces;
using DebeziumPoc.Api.Application.Service.Abstractions;
using DebeziumPoc.Api.Application.Service.Implementations;
using DebeziumPoc.Api.Domain.Repositories;
using DebeziumPoc.Api.Infrastructure.Configurations;
using DebeziumPoc.Api.Infrastructure.Context;
using DebeziumPoc.Api.Infrastructure.Kafka;
using DebeziumPoc.Api.Infrastructure.Repositories;
using DebeziumPoc.Api.Infrastructure.Services;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

builder.Host.ConfigureLogging((hostingContext, logging) =>
{
    logging.AddConfiguration(hostingContext.Configuration.GetSection("Logging"));
    logging.AddConsole();
    logging.AddDebug();
    logging.AddEventSourceLogger();
});

// Add services to the container.
builder.Services.Configure<KafkaConfiguration>(builder.Configuration.GetSection(nameof(KafkaConfiguration)));
builder.Services.Configure<MongoDbConfiguration>(builder.Configuration.GetSection(nameof(MongoDbConfiguration)));
builder.Services.AddSingleton<IMongoDbConfiguration>(serviceProvider => serviceProvider.GetRequiredService<IOptions<MongoDbConfiguration>>().Value);
builder.Services.AddSingleton<IMongoDBContext, MongoDBContext>();

builder.Services.AddSingleton<IEventConsumer, KafkaConsumer>();

builder.Services.AddTransient<IProductRepository,ProductRepository>();
builder.Services.AddTransient<IProductService, ProductService>();

builder.Services.AddHostedService<KafkaConsumerService>();



builder.Services.AddControllers();
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

app.UseCors();

app.MapControllers();

app.Run();
