namespace ListOverLimitCustomersExtended;

public class Customer
{
    public string Name { get; set; }
    public string Address { get; set; }
    public string Email { get; set; }
    public decimal CreditLimit { get; set; }
}

public class ListOverLimitCustomers
{
    private readonly decimal creditLimitThreshold = 500;
    public void After(string parameter)
    {
        Console.WriteLine(parameter);

        var overLimitCustomers = new List<Customer>();

        using var doc = JsonDocument.Parse(parameter);
        var element = doc.RootElement;

        foreach (var eachElement in element.EnumerateArray())
        {
            var customer = new Customer
            {
                Name = eachElement.GetProperty("Name").GetString(),
                Address = eachElement.GetProperty("Address").GetString(),
                Email = eachElement.GetProperty("Email").GetString(),
                CreditLimit = eachElement.GetProperty("CreditLimit").GetDecimal()
            };

            if (customer.CreditLimit > creditLimitThreshold)
            {
                overLimitCustomers.Add(customer);
            }
        }

        Console.WriteLine("Danh sách customer vượt ngưỡng:");

        foreach (var customer in overLimitCustomers)
        {
            Console.WriteLine(
                $"Name: {customer.Name}, Address: {customer.Address}, Email: {customer.Email}, CreditLimit: {customer.CreditLimit}");
        }

        Console.WriteLine($"Tổng số customer thỏa điều kiện: {overLimitCustomers.Count}");

    }
}
