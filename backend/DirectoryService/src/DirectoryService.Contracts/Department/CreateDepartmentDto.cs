namespace DirectoryService.Contracts.Department;

sealed public record CreateDepartmentDto(string Name, string Path, Guid ParentId, string Slug);