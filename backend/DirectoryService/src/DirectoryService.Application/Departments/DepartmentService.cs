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
    private readonly IValidator<CreateDepartmentDto> _сreateDepartmentValidator;
    private readonly IValidator<CreateDepartmentRelationDto> _createDepartmentRelationValidator;

    public DepartmentService(ILogger<DepartmentService> logger, IDepartmentRepository departmentRepository, IValidator<CreateDepartmentDto> createDepartmentValidator, IValidator<CreateDepartmentRelationDto> createDepartmentRelationValidator)
    {
        _logger = logger;
        _departmentRepository = departmentRepository;
        _сreateDepartmentValidator = createDepartmentValidator;
        _createDepartmentRelationValidator = createDepartmentRelationValidator;
    }

    public async Task<Guid> CreateAsync(CreateDepartmentDto departmentDto, CancellationToken cancellationToken)
    {
        var validationResult = await _сreateDepartmentValidator.ValidateAsync(departmentDto, cancellationToken);

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
            departmentPath = await _departmentRepository.GetPathByIdAsync(departmentDto.ParentId.Value, cancellationToken);
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
        var department = await _departmentRepository.UpdateNameAsync(departmentId, cancellationToken);
        department.UpdateDepartmentName(departmentName);
        _departmentRepository.SaveChangesAsync(cancellationToken);
        _logger.LogInformation("Updating department with id {DepartmentName}", departmentDto.DepartmentName);

        //add pattern result for response
        return true;
    }

    public async Task<Department> GetByIdAsync(Guid departmentId, CancellationToken cancellationToken)
    {
        var department = await _departmentRepository.GetByIdAsync(departmentId, cancellationToken);
        if (department == null)
        {
            throw new ArgumentException($"Department with id {departmentId} not found.", nameof(departmentId));
        }

        _logger.LogInformation("Retrieving department with id {DepartmentId}", departmentId);
        return department;
    }

    public async Task<IReadOnlyCollection<Department>> GetDepartmentsAsync(CancellationToken cancellationToken)
    {
        var departments = await _departmentRepository.GetAllAsync(cancellationToken);
        return departments;
    }

    public async Task<bool> CreateRelation(CreateDepartmentRelationDto departmentRelationDto, CancellationToken cancellationToken)
    {
        var validationResult = await _createDepartmentRelationValidator.ValidateAsync(departmentRelationDto, cancellationToken);
        if (!validationResult.IsValid)
        {
            _logger.LogError("Invalid department relation data provided.");
            throw new ValidationException(validationResult.Errors);
        }

        var department = await _departmentRepository.GetByIdAsync(departmentRelationDto.DepartmentId, cancellationToken);
        if (department == null)
        {
            throw new ArgumentException($"Department with id {departmentRelationDto.DepartmentId} not found.", nameof(departmentRelationDto));
        }

        var added = department.AddLocation(departmentRelationDto.LocationId);

        await _departmentRepository.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Creating relation between department {DepartmentId} and location {LocationId}", departmentRelationDto.DepartmentId, departmentRelationDto.LocationId);
        return added;
    }

    public async Task<bool> DeleteRelation(DeleteDepartmentRelationDto departmentRelationDto, CancellationToken cancellationToken)
    {
        var department = await _departmentRepository.GetByIdAsync(departmentRelationDto.DepartmentId, cancellationToken);
        if (department == null)
        {
            throw new ArgumentException($"Department with id {departmentRelationDto.DepartmentId} not found.", nameof(departmentRelationDto));
        }
        var removed = department.RemoveLocation(departmentRelationDto.LocationId);
        await _departmentRepository.SaveChangesAsync(cancellationToken);
        _logger.LogInformation("Deleting relation between department {DepartmentId} and location {LocationId}", departmentRelationDto.DepartmentId, departmentRelationDto.LocationId);
        return removed;
    }

    public async Task<bool> DeleteAsync(Guid DepartmentId, CancellationToken cancellationToken)
    {
        var result = await _departmentRepository.DeleteAsync(DepartmentId, cancellationToken);
        _logger.LogInformation("Deleting department with id {DepartmentId}", DepartmentId);
        return result;
    }
}