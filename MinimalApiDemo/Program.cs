using MinimalApiDemo.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

// In-memory customer storage
var customers = new List<Customer>();

// Get all customers
app.MapGet("/customers", () => customers);

// Get a customer by ID
app.MapGet("/customers/{id}", (Guid id) =>
{
	var customer = customers.FirstOrDefault(c => c.Id == id);
	return customer is not null
		  ? Results.Ok(customer)
		  : Results.NotFound($"Customer with ID {id} not found.");
});

// Add a new customer
app.MapPost("/customers", (Customer customer) =>
{
	customers.Add(customer);
	return Results.Created($"/customers/{customer.Id}", customer);
});

// Update an existing customer
app.MapPut("/customers/{id}", (Guid id, Customer updatedCustomer) =>
{
	var customer = customers.FirstOrDefault(c => c.Id == id);
	if (customer is null)
		return Results.NotFound($"Customer with ID {id} not found.");

	customer.Name = updatedCustomer.Name;
	customer.Email = updatedCustomer.Email;
	customer.Phone = updatedCustomer.Phone;

	return Results.Ok(customer);
});

// Delete a customer
app.MapDelete("/customers/{id}", (Guid id) =>
{
	var customer = customers.FirstOrDefault(c => c.Id == id);
	if (customer is null)
		return Results.NotFound($"Customer with ID {id} not found.");

	customers.Remove(customer);
	return Results.NoContent();
});

// Root endpoint
app.MapGet("/", () => "Welcome to the Customer API!");

app.Run();

