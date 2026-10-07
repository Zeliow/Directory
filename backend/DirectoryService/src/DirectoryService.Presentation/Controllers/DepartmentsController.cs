using DirectoryService.Application.Departments;
using DirectoryService.Contracts.Department;
using Microsoft.AspNetCore.Mvc;

namespace DirectoryService.Presentation.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DepartmentsController : ControllerBase
{
    private readonly IDepartmentService _departmentsService;

    public DepartmentsController(IDepartmentService departmentsService)
    {
        _departmentsService = departmentsService;
    }

    [HttpGet]
    public async Task<IActionResult> GetDepartments(CancellationToken cancellationToken)
    {
        var departments = await _departmentsService.GetDepartmentsAsync(cancellationToken);
        return Ok(departments);
    }

    [HttpPost]
    public async Task<IActionResult> CreateDepartment(
       [FromBody] CreateDepartmentDto departmentDto,
       CancellationToken cancellationToken)
    {
        var departmentId = await _departmentsService.CreateAsync(departmentDto, cancellationToken);
        return Ok(departmentId);
    }

    [HttpPatch("{id::guid}")]
    public async Task<IActionResult> UpdateDepartment(
       [FromRoute] Guid id,
       [FromBody] UpdateDepartmentDto departmentDto,
       CancellationToken cancellationToken)
    {
        var result = await _departmentsService.UpdateAsync(id, departmentDto, cancellationToken);
        if (result)
        {
            return Ok(new { Message = $"Department with ID {id} updated" });
        }
        else
        {
            return BadRequest(new { Message = $"Failed to update department with ID {id}" });
        }
    }

    [HttpPost("{id::guid}/locations/{locationId::guid}")]
    public async Task<IActionResult> UpdateRelations(
        [FromRoute] Guid id,
        [FromRoute] Guid locationId,
        CancellationToken cancellationToken)
    {
        var createDepartmentRelationDto = new CreateDepartmentRelationDto(LocationId: locationId, DepartmentId: id);
        await _departmentsService.CreateRelation(createDepartmentRelationDto, cancellationToken);
        return Ok($"Relation of department {id} created.");
    }

    [HttpDelete("{id::guid}/locations/{locationId::guid}")]
    public async Task<IActionResult> DeleteRelations(
        [FromRoute] Guid id,
        [FromRoute] Guid locationId,
        CancellationToken cancellationToken)
    {
        var deleteDepartmentRelationDto = new DeleteDepartmentRelationDto(LocationId: locationId, DepartmentId: id);
        await _departmentsService.DeleteRelation(deleteDepartmentRelationDto, cancellationToken);
        return Ok($"Relation of department {id} deleted.");
    }

    [HttpGet("{id::guid}")]
    public async Task<IActionResult> GetDepartmentById(
        [FromRoute] Guid id,
        CancellationToken cancellationToken)
    {
        var department = await _departmentsService.GetByIdAsync(id, cancellationToken);
        if (department == null)
        {
            return NotFound();
        }
        return Ok(department);
    }

    [HttpDelete("{id::guid}")]
    public async Task<IActionResult> DeleteDepartment(
        [FromRoute] Guid id,
        CancellationToken cancellationToken)
    {
        var result = await _departmentsService.DeleteAsync(id, cancellationToken);
        if (!result)
        {
            return NotFound();
        }
        return Ok(new { Message = $"Department with ID {id} deleted" });
    }
}