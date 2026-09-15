namespace DirectoryService.Contracts.Department;

sealed public record UpdateDepartmentDto(string DepartmentName, IEnumerable<Guid> LocationIds);