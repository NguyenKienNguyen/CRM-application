// src/CRM.Core/Services/CustomerService.cs
public interface ICustomerService
{
    Task<List<CustomerModel>> GetAllAsync();
    Task<CustomerModel> CreateAsync(CustomerModel customer);
    Task UpdateAsync(int id, CustomerModel customer);
    Task DeleteAsync(int id);
    Task CommitAsync(List<CustomerModel> customers);   // Chạy hooks
}

public class CustomerService : ICustomerService
{
    private readonly List<CustomerModel> _customers = new();
    private readonly PluginLoader _pluginLoader;

    public CustomerService(PluginLoader pluginLoader)
    {
        _pluginLoader = pluginLoader;
    }

    public async Task CommitAsync(List<CustomerModel> customers)
    {
        // Chạy tất cả plugin (hook)
        foreach (var plugin in _pluginLoader.GetPlugins())
        {
            plugin.After(JsonSerializer.Serialize(customers));   // Hook theo sách
        }
        // Lưu DB thật (ở đây dùng InMemory)
        await Task.CompletedTask;
    }

    // Các method GetAll, Create, Update, Delete... (thêm logic CRUD đơn giản)
}