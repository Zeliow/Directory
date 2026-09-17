using DirectoryService.Application.Interfaces;
using DirectoryService.Contracts.Department;
using FluentValidation;

namespace DirectoryService.Application.Validators;

public class CreateDepartmentRelationValidator : AbstractValidator<CreateDepartmentRelationDto>
{
    public CreateDepartmentRelationValidator(ILocationRepository locationRepository)
    {
        RuleFor(x => x.DepartmentId)
            .NotEmpty().WithMessage("Недопустимо пустое значение идентификатора отдела");

        RuleFor(x => Enumerable.Repeat(x.LocationId, 1))
            .NotEmpty().WithMessage("Недопустимо пустое значение идентификатора локации")
            .MustAsync(locationRepository.IsValidLocationsAsync).WithMessage("Указанная локация не существует");
    }
}