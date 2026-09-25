using DirectoryService.Application.Departments.Failure;
using DirectoryService.Application.Helpers;
using DirectoryService.Application.Interfaces;
using DirectoryService.Application.Locations.Failure;
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
    private readonly ILocationRepository _locationRepository;
    private readonly IValidator<CreateDepartmentDto> _сreateDepartmentValidator;
    private readonly IValidator<CreateDepartmentRelationDto> _createDepartmentRelationValidator;

    public DepartmentService(
        ILogger<DepartmentService> logger,
        IDepartmentRepository departmentRepository,
        ILocationRepository locationRepository,
        IValidator<CreateDepartmentDto> createDepartmentValidator,
        IValidator<CreateDepartmentRelationDto> createDepartmentRelationValidator)
    {
        _logger = logger;
        _departmentRepository = departmentRepository;
        _locationRepository = locationRepository;
        _сreateDepartmentValidator = createDepartmentValidator;
        _createDepartmentRelationValidator = createDepartmentRelationValidator;
    }

    public async Task<Guid> CreateAsync(CreateDepartmentDto departmentDto, CancellationToken cancellationToken)
    {
        var validationResult = await _сreateDepartmentValidator.ValidateAsync(departmentDto, cancellationToken);

        if (!validationResult.IsValid)
        {
            _logger.LogError("Invalid department data provided.");

            throw new DepartmentValidationException(validationResult.ToErrors());
        }

        var locationsAreValid = await _locationRepository.IsValidLocationsAsync(departmentDto.LocationIds, cancellationToken);
        if (!locationsAreValid)
        {
            _logger.LogError("One or more locations do not exist.");
            throw new LocationNotFoundException(departmentDto.LocationIds);
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
            throw new DepartmentParentIsNotExistException();
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
        await _departmentRepository.SaveChangesAsync(cancellationToken);
        _logger.LogInformation("Updating department with id {DepartmentName}", departmentDto.DepartmentName);

        return true;
    }

    public async Task<Department> GetByIdAsync(Guid departmentId, CancellationToken cancellationToken)
    {
        var department = await _departmentRepository.GetByIdAsync(departmentId, cancellationToken);
        if (department == null)
        {
            throw new DepartmentNotExistException(departmentId);
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
            throw new DepartmentValidationException(validationResult.ToErrors());
        }

        var department = await _departmentRepository.GetByIdAsync(departmentRelationDto.DepartmentId, cancellationToken);
        if (department == null)
        {
            throw new DepartmentNotExistException(departmentRelationDto.DepartmentId);
        }

        var location = await _locationRepository.GetByIdAsync(departmentRelationDto.LocationId, cancellationToken);
        if (location == null)
        {
            throw new LocationNotFoundException(departmentRelationDto.LocationId);
        }

        var added = department.AddLocation(departmentRelationDto.LocationId);
        if (!added)
        {
            throw new DepartmentLocationAlreadyExistsException(departmentRelationDto.LocationId);
        }

        await _departmentRepository.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Creating relation between department {DepartmentId} and location {LocationId}", departmentRelationDto.DepartmentId, departmentRelationDto.LocationId);
        return true;
    }

    public async Task<bool> DeleteRelation(DeleteDepartmentRelationDto departmentRelationDto, CancellationToken cancellationToken)
    {
        var department = await _departmentRepository.GetByIdAsync(departmentRelationDto.DepartmentId, cancellationToken);
        if (department == null)
        {
            throw new DepartmentNotExistException(departmentRelationDto.DepartmentId);
        }

        var removed = department.RemoveLocation(departmentRelationDto.LocationId);
        if (!removed)
        {
            throw new DepartmentLocationNotExistsException(departmentRelationDto.LocationId);
        }

        await _departmentRepository.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Deleting relation between department {DepartmentId} and location {LocationId}", departmentRelationDto.DepartmentId, departmentRelationDto.LocationId);
        return true;
    }

    public async Task<bool> DeleteAsync(Guid DepartmentId, CancellationToken cancellationToken)
    {
        var result = await _departmentRepository.DeleteAsync(DepartmentId, cancellationToken);
        _logger.LogInformation("Deleting department with id {DepartmentId}", DepartmentId);
        return result;
    }
}