// See https://aka.ms/new-console-template for more information
using Hino.Service.AnyMarket.Tests;
using System;
using System.Text.Json;

var person = new Person()
{
    Name ="Jona",
    Id = 1,
    FirstName = "Jonathan",
    Address = new Address()
    {
        City = "Limeira",
        State = "SP",
        PostalCode = "13481035"
    }
};

// Serialize the person to JSON.
JsonSerializerOptions options = new JsonSerializerOptions();
options.Converters.Add(new PersonConverter());

string jsonString = JsonSerializer.Serialize(person, options);

Console.WriteLine(jsonString);

