using DirectoryService.Shared;

namespace DirectoryService.Application.Departments.Failure;

public static partial class Errors
{
    public static class DepartmentErrors
    {
        public static Error ParentNotFound()
        {
            return Error.NotFound("department.parent.does.not.exist", "The parent department does not exist.");
        }
    }
}