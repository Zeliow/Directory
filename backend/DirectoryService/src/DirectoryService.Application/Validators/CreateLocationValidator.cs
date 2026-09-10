using DirectoryService.Contracts.Location;
using FluentValidation;

namespace DirectoryService.Application.Validators;

sealed public class CreateLocationValidator : AbstractValidator<CreateLocationDto>
{
    public CreateLocationValidator()
    {
        RuleFor(x => x.LocationName)
            .NotEmpty().WithMessage("Недопустимо пустое значение имени локации")
            .MaximumLength(100)
            .MinimumLength(10);

        RuleFor(x => x.AddressDto.Country)
            .NotEmpty().WithMessage("Недопустимо пустое значение страны")
            .MaximumLength(100)
            .MinimumLength(10);

        RuleFor(x => x.AddressDto.City)
            .NotEmpty().WithMessage("Недопустимо пустое значение города")
            .MaximumLength(100)
            .MinimumLength(10);

        RuleFor(x => x.AddressDto.Street)
           .NotEmpty().WithMessage("Недопустимо пустое значение улицы")
           .MaximumLength(100)
           .MinimumLength(10);
    }
}