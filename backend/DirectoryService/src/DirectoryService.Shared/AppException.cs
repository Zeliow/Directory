namespace DirectoryService.Shared;

public abstract class AppException : Exception
{
    public Error Error { get; }

    protected AppException(Error error) : base(error.Message)
    {
        Error = error;
    }
}