using DirectoryService.Shared;

namespace DirectoryService.Application.Exceptions;

public class NotFoundException : AppException
{
    public NotFoundException(Error[] errors) : base(errors)
    {
    }
}
