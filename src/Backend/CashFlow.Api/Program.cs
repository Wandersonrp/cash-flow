using CashFlow.Api.Filters;
using CashFlow.Api.Middlewares;
using CashFlow.Infrastructure;
using CashFlow.Application;
using CashFlow.Api.Converter;
using Microsoft.OpenApi.Models;
using System.Runtime.CompilerServices;
using CashFlow.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddApplication();

// Add services to the container.
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.Converters.Add(new DateOnlyJsonConverter());
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(config =>
{
    config.MapType<DateOnly>(() => new OpenApiSchema { Type = "string", Format = "date" });
});

builder.Services.AddRouting(options => options.LowercaseUrls = true);

builder.Services.AddMvc(options => 
{
    options.Filters.Add(typeof(ExceptionFilter));
});

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "CashFlowWeb", policy =>
    {
        policy.WithOrigins("http://localhost:5109/", "https://localhost:7218");
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

InitializeDbContext.Initialize(app);

app.UseMiddleware<CultureMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();


internal static class InitializeDbContext
{    
    internal static void Initialize(WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var services = scope.ServiceProvider;
        var context = services.GetRequiredService<CashFlowDbContext>();
        context.Expenses.Count();
    }
}