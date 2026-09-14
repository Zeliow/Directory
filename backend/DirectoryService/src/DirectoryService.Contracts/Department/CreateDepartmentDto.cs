namespace DirectoryService.Contracts.Department;

sealed public record CreateDepartmentDto(string Name, Guid ParentId, string Slug, IEnumerable<Guid> LocationIds);