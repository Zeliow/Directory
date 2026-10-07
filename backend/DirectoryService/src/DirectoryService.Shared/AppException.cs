namespace DirectoryService.Shared;

public abstract class AppException : Exception
{
    public IReadOnlyCollection<Error> Errors { get; }

    protected AppException(params Error[] errors)
        : base(errors.Length > 0 ? errors[0].Message : "Ошибка приложения")
    {
        Errors = errors;
    }
}
