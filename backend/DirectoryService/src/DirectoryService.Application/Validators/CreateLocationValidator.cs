using DirectoryService.Contracts.Location;
using FluentValidation;

namespace DirectoryService.Application.Validators;

sealed public class CreateLocationValidator : AbstractValidator<CreateLocationDto>
{
    public CreateLocationValidator()
    {
        RuleFor(x => x.LocationName).NotEmpty();
        RuleFor(x => x.AddressDto).NotEmpty();
    }
}