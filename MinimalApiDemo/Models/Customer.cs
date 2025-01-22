namespace MinimalApiDemo.Models;

public class Customer
{
	public Guid Id { get; private set; } = Guid.NewGuid();
	public string Name { get; set; }
	public string Email { get; set; }
	public string Phone { get; set; }
	public string Url { get; private set; }

	public void SetUrl(string url) => Url = url;
}
