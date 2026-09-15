using DirectoryService.Application.Interfaces;
using DirectoryService.Contracts.Department;
using FluentValidation;

namespace DirectoryService.Application.Validators;

public class CreateDepartmentValidator : AbstractValidator<CreateDepartmentDto>
{
    public CreateDepartmentValidator(ILocationRepository locationRepository)
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Недопустимо пустое значение имени отдела")
            .MaximumLength(100)
            .MinimumLength(4);

        RuleFor(x => x.Slug)
            .NotEmpty().WithMessage("Недопустимо пустое значение slug")
            .MaximumLength(100)
            .MinimumLength(4);

        RuleFor(x => x.LocationIds)
            .NotEmpty()
            .MustAsync(locationRepository.IsValidLocationsAsync)
            .WithMessage("Одна или несколько указанных локаций не существуют.");
    }
}