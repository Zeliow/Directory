using DirectoryService.Application.Interfaces;
using DirectoryService.Contracts.Department;
using DirectoryService.Domain;
using DirectoryService.Domain.DepartmentVO;
using FluentValidation;
using Microsoft.Extensions.Logging;

namespace DirectoryService.Application.Departments;

public class DepartmentService : IDepartmentService
{
    private readonly ILogger<DepartmentService> _logger;
    private readonly IDepartmentRepository _departmentRepository;
    private readonly IValidator<CreateDepartmentDto> _CreateDepartmentValidator;

    public DepartmentService(ILogger<DepartmentService> logger, IDepartmentRepository departmentRepository, IValidator<CreateDepartmentDto> createDepartmentValidator)
    {
        _logger = logger;
        _departmentRepository = departmentRepository;
        _CreateDepartmentValidator = createDepartmentValidator;
    }

    public async Task<Guid> CreateAsync(CreateDepartmentDto departmentDto, CancellationToken cancellationToken)
    {
        var validationResult = await _CreateDepartmentValidator.ValidateAsync(departmentDto, cancellationToken);

        if (!validationResult.IsValid)
        {
            _logger.LogError("Invalid department data provided.");
            throw new ValidationException(validationResult.Errors);
        }

        var departmentName = DepartmentName.Create(departmentDto.Name);
        _logger.LogInformation("Department name is correct!");

        var departmentSlug = Slug.Create(departmentDto.Slug);
        _logger.LogInformation("Department slug is correct!");

        DepartmentPath? departmentPath = null;
        if (departmentDto.ParentId != null)
        {
            departmentPath = await _departmentRepository.GetByIdAsync(departmentDto.ParentId.Value, cancellationToken);
        }
        if (departmentPath == null && departmentDto.ParentId != null)
        {
            _logger.LogError("Parent department with id {ParentId} not found.", departmentDto.ParentId);
            throw new ArgumentException($"Parent department with id {departmentDto.ParentId} not found.", nameof(departmentDto));
        }
        var parentId = ParentId.Create(departmentDto.ParentId);

        var department = Department.Create(departmentName, departmentSlug, departmentPath, parentId, departmentDto.LocationIds);

        _logger.LogInformation("Success create department with id {Id}", department.Id);
        await _departmentRepository.AddAsync(department, cancellationToken);
        return department.Id;
    }

    public async Task<bool> UpdateAsync(Guid departmentId, UpdateDepartmentDto departmentDto, CancellationToken cancellationToken)
    {
        var departmentName = DepartmentName.Create(departmentDto.DepartmentName);
        var result = await _departmentRepository.UpdateNameAsync(departmentId, departmentName, cancellationToken);
        _logger.LogInformation("Updating department with id {DepartmentName}", departmentDto.DepartmentName);
        return result;
    }

    public async Task<Guid> DeleteAsync(Guid DepartmentId, CancellationToken cancellationToken)
    {
        // Implementation for deleting a department
        _logger.LogInformation("Deleting department with id {DepartmentId}", DepartmentId);
        return Guid.CreateVersion7();
    }

    public async Task<Department> GetByIdAsync(Guid departmentId, CancellationToken cancellationToken)
    {
        // Implementation for retrieving a department by ID
        _logger.LogInformation("Retrieving department with id {DepartmentId}", departmentId);
        return Department.Create(DepartmentName.Create("Sample Department"), Slug.Create("sample-department"), null, null, new List<Guid>());
    }

    public async Task<IReadOnlyCollection<Department>> GetDepartmentsAsync(CancellationToken cancellationToken)
    {
        var departments = await _departmentRepository.GetAllAsync(cancellationToken);
        return departments;
    }

    public Task<bool> CreateRelation(Guid departmentId, Guid locationId, CancellationToken cancellationToken)
    {
        var department = _departmentRepository.GetByIdAsync(departmentId, cancellationToken);

        var
        // Implementation for creating a relation between a department and a location
        _logger.LogInformation("Creating relation between department {DepartmentId} and location {LocationId}", departmentId, locationId);
        return Task.FromResult(true);
    }
}