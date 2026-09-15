using DirectoryService.Contracts.Department;
using FluentValidation;

namespace DirectoryService.Application.Validators;

public class CreateDepartmentValidator : AbstractValidator<CreateDepartmentDto>
{
    public CreateDepartmentValidator()
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
            .Must(x => x != null && x.Any()).WithMessage("LocationIds не может быть пустым");
    }
}