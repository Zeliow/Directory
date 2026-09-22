using DirectoryService.Shared;
using System.Text.Json;

namespace DirectoryService.Application.Exceptions;

public class BadRequestException : Exception
{
    protected BadRequestException(Error[] errors) : base(JsonSerializer.Serialize(errors))
    {
    }
}