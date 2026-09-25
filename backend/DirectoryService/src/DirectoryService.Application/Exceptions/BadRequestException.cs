using DirectoryService.Shared;

namespace DirectoryService.Application.Exceptions;

public class BadRequestException : AppException
{
    public BadRequestException(Error[] errors) : base(errors)
    {
    }
}
