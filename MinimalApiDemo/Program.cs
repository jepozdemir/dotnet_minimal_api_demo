using MinimalApiDemo.Endpoints;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.MapCustomerEndpoints();

// Root endpoint
app.MapGet("/", () => "Welcome to the Customer API!");

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.Run();

