using System.Text.Json.Serialization;

namespace DirectoryService.Shared;

public sealed record Error
{
    public string Code { get; }
    public string Message { get; }
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public ErrorType Type { get; }

    [JsonConstructor]
    private Error(string code, string message, ErrorType type)
    {
        Code = code;
        Message = message;
        Type = type;
    }

    public static Error Validation(string? code, string message) => new(code ?? "value.invalid", message, ErrorType.VALIDATION);
    public static Error NotFound(string? code, string message) => new(code ?? "value.not.found", message, ErrorType.NOT_FOUND);
    public static Error Conflict(string? code, string message) => new(code ?? "value.is.conflict", message, ErrorType.CONFLICT);
    public static Error Failure(string? code, string message) => new(code ?? "value.failed", message, ErrorType.FAILURE);
}

public enum ErrorType
{
    /// <summary>
    /// Error type for validation errors, such as invalid input or missing required fields.
    /// </summary>
    VALIDATION,

    /// <summary>
    /// Error type for not found errors, indicating that a requested resource could not be found.
    /// </summary>
    NOT_FOUND,

    /// <summary>
    /// Error type for conflict errors, indicating that a requested operation could not be completed due to a conflict with the current state of the resource.
    /// </summary>
    CONFLICT,

    /// <summary>
    /// Error type for general failure errors, indicating that an operation could not be completed due to an unexpected error or exception.
    /// </summary>
    FAILURE,
}