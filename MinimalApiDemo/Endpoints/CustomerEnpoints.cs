using MinimalApiDemo.Models;
using MinimalApiDemo.Services;

namespace MinimalApiDemo.Endpoints;

public static class CustomerEnpoints
{
	public static void MapCustomerEndpoints(this WebApplication app)
	{
		var customers = app.MapGroup("api/customers");

		// Get a customer by ID
		customers.MapGet("/{id:guid}", (Guid id) =>
		{
			var customer = CustomerService.GetById(id);
			return customer is not null
				  ? Results.Ok(customer)
				  : Results.NotFound($"Customer with ID {id} not found.");
		})
		.WithName("CustomerById");

		// Get all customers
		customers.MapGet("/", () => CustomerService.Customers).WithName("CustomerList");

		// Add a new customer
		customers.MapPost("/", (Customer customer, LinkGenerator linker) =>
		{
			customer.SetUrl(linker.GetPathByName("CustomerById", new { id = customer.Id }));
			CustomerService.Add(customer);
			return Results.Created(customer.Url, customer);
		})
		.WithName("CreateCustomer");

		// Update an existing customer
		customers.MapPut("/{id:guid}", (Guid id, Customer updatedCustomer) =>
		{
			var customer = CustomerService.GetById(id);
			if (customer is null)
				return Results.NotFound($"Customer with ID {id} not found.");

			CustomerService.Update(id, updatedCustomer);

			return Results.Ok(customer);
		})
		.WithName("UpdateCustomer");

		// Delete a customer
		customers.MapDelete("/{id:guid}", (Guid id) =>
		{
			var customer = CustomerService.GetById(id);
			if (customer is null)
				return Results.NotFound($"Customer with ID {id} not found.");

			CustomerService.Delete(customer);
			return Results.NoContent();
		})
		.WithName("DeleteCustomer");
	}
}
