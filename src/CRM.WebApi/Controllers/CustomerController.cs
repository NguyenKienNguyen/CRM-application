// src/CRM.WebApi/Controllers/CustomerController.cs
[ApiController]
[Route("api/customers")]
public class CustomerController : ControllerBase
{
    private readonly ICustomerService _service;

    public CustomerController(ICustomerService service) => _service = service;

    [HttpGet] public async Task<IActionResult> GetAll() => Ok(await _service.GetAllAsync());
    [HttpPost] public async Task<IActionResult> Create([FromBody] CustomerModel model) => Ok(await _service.CreateAsync(model));
    [HttpPut("{id}")] public async Task<IActionResult> Update(int id, [FromBody] CustomerModel model) { await _service.UpdateAsync(id, model); return NoContent(); }
    [HttpDelete("{id}")] public async Task<IActionResult> Delete(int id) { await _service.DeleteAsync(id); return NoContent(); }

    [HttpPost("commit")]
    public async Task<IActionResult> Commit([FromBody] List<CustomerModel> customers)
    {
        await _service.CommitAsync(customers);
        return Ok(new { message = "Commit thành công - Plugins đã chạy!" });
    }
}