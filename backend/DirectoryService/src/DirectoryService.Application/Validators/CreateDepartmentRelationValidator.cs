using DirectoryService.Contracts.Department;
using FluentValidation;

namespace DirectoryService.Application.Validators;

public class CreateDepartmentRelationValidator : AbstractValidator<CreateDepartmentRelationDto>
{
    public CreateDepartmentRelationValidator()
    {
        RuleFor(x => x.DepartmentId)
            .NotEmpty().WithMessage("Недопустимо пустое значение идентификатора отдела");

        RuleFor(x => x.LocationId)
            .NotEmpty().WithMessage("Недопустимо пустое значение идентификатора локации");
    }
}
