using DirectoryService.Shared;
using System.Text.Json;

namespace DirectoryService.Application.Exceptions;

public class NotFoundException : Exception
{
    protected NotFoundException(Error[] errors) : base(JsonSerializer.Serialize(errors))
    {
    }
}