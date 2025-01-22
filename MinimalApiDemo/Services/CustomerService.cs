using MinimalApiDemo.Models;

namespace MinimalApiDemo.Services;

public static class CustomerService
{
	// In-memory customer storage
	public static List<Customer> Customers { get; } = new();

	public static Customer? GetById(Guid id) => Customers.FirstOrDefault(c => c.Id == id);

	public static void Add(Customer customer) => Customers.Add(customer);

	public static void Update(Guid id, Customer updatedCustomer)
	{
		var customer = Customers.FirstOrDefault(c => c.Id == id);
		if (customer is not null)
		{
			customer.Name = updatedCustomer.Name;
			customer.Email = updatedCustomer.Email;
			customer.Phone = updatedCustomer.Phone;
		}
	}

	public static void Delete(Guid id)
	{
		var customer = Customers.FirstOrDefault(c => c.Id == id);
		Delete(customer);
	}

	public static void Delete(Customer customer)
	{
		if (customer is not null)
		{
			Customers.Remove(customer);
		}
	}
}
