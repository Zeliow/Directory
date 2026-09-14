using DirectoryService.Application;
using DirectoryService.Contracts.Department;
using Microsoft.AspNetCore.Mvc;

namespace DirectoryService.Presentation.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DepartmentsController : ControllerBase
{
    private readonly IDepartmentsService _departmentsService;

    public DepartmentsController(IDepartmentsService departmentsService)
    {
        _departmentsService = departmentsService;
    }

    [HttpGet]
    public async Task<IActionResult> GetDepartments(CancellationToken cancellationToken)
    {
        // Logic to retrieve departments would go here
        return Ok(Array.Empty<string>());
    }

    [HttpGet("{id::guid}")]
    public async Task<IActionResult> GetDepartmentById(
        [FromRoute] Guid id,
        CancellationToken cancellationToken)
    {
        // Logic to retrieve a specific department by ID would go here
        return NotFound();
    }

    [HttpPost]
    public async Task<IActionResult> CreateDepartment(
        [FromBody] CreateDepartmentDto departmentDto,
        CancellationToken cancellationToken)
    {
        var departmentId = await _departmentsService.CreateAsync(departmentDto, cancellationToken);
        // Logic to create a new department would go here
        return Ok(departmentId);
    }

    [HttpPut("{id::guid}")]
    public async Task<IActionResult> UpdateDepartment(
        [FromRoute] Guid id,
        [FromBody] UpdateDepartmentDto departmentDto,
        CancellationToken cancellationToken)
    {
        // Logic to update a specific department by ID would go here
        return Ok(new { Message = $"Department with ID {id} updated" });
    }

    [HttpDelete("{id::guid}")]
    public async Task<IActionResult> DeleteDepartment(
        [FromRoute] Guid id,
        CancellationToken cancellationToken)
    {
        // Logic to delete a specific department by ID would go here
        return Ok(new { Message = $"Department with ID {id} deleted" });
    }
}