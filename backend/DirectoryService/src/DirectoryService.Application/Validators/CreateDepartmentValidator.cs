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
            .NotNull().WithMessage("Недопустимо пустое значение LocationIds")
            .MustAsync(async (locationIds, cancellationToken) =>
            {
                if (locationIds == null || !locationIds.Any())
                {
                    return false;
                }

                foreach (var locationId in locationIds)
                {
                    if (!await locationRepository.IsValidLocationsAsync(new List<Guid> { locationId }, cancellationToken))
                    {
                        return false;
                    }
                }

                return true;
            }).WithMessage("LocationIds не существует в базе данных");
    }
}