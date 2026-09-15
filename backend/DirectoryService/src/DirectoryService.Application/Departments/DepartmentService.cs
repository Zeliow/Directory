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

        var parentId = ParentId.Create(departmentDto.ParentId);

        var department = Department.Create(departmentName, departmentSlug, departmentPath, parentId, departmentDto.LocationIds);

        _logger.LogInformation("Success create department with id {Id}", department.Id);
        await _departmentRepository.AddAsync(department, cancellationToken);
        return department.Id;
    }
}