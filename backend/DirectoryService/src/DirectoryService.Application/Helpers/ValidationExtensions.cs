using DirectoryService.Shared;
using FluentValidation.Results;

namespace DirectoryService.Application.Helpers;

public static class ValidationExtensions
{
    public static Error[] ToErrors(this ValidationResult validationResult)
    {
        return validationResult.Errors.Select(e => Error.Validation(e.ErrorCode, e.ErrorMessage)).ToArray();
    }
}