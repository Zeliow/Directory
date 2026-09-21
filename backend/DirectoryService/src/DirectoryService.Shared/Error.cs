namespace DirectoryService.Shared;

public sealed record Error
{
    public string Code { get; }
    public string Message { get; }
    public ErrorType Type { get; }

    private Error(string code, string message, ErrorType type)
    {
        Code = code;
        Message = message;
        Type = type;
    }

    public static Error Validation(string? code, string message) => new(code ?? "value.invalid", message, ErrorType.VALIDATION);
    public static Error NotFound(string? code, string message) => new(code ?? "value.not.found", message, ErrorType.NOT_FOUND);
    public static Error Confict(string? code, string message) => new(code ?? "value.is.conflict", message, ErrorType.CONFLICT);
    public static Error Failure(string? code, string message) => new(code ?? "value.failed", message, ErrorType.FAILURE);
}

public enum ErrorType
{
    VALIDATION,
    NOT_FOUND,
    CONFLICT,
    FAILURE,
}