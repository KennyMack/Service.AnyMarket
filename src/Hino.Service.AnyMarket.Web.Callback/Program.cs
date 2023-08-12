using Hino.Service.AnyMarket.Application.Orders.Interfaces;
using Hino.Service.AnyMarket.Application.Orders.Services;
using Hino.Service.AnyMarket.IoC;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

const string originName = "OriginAnyMarket";

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: originName,
                      policy =>
                      {
                          policy.WithOrigins("*")
                          .AllowAnyMethod()
                          .AllowAnyHeader();
                      });
});

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpClient();
builder.Services.RegisterServices();

var app = builder.Build();

app.UseCors(originName);
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapPost("/orders/callback", ([FromServices]IOrderReceivedAS orderReceivedAS, IHttpClientFactory httpClientFactory) =>
{
    var client = httpClientFactory.CreateClient();
    var json = "{\"name\":\"abc\" }";

    // generate result ok json body
    return Results.Ok(json);
})
.WithName("PostOrdersCallback");

app.Run();
