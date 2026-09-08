using DirectoryService.Domain;
using DirectoryService.Domain.DepartmentVO;

namespace DirectoryService.Contracts.Department;

sealed public record CreateDepartmentDto(DepartmentName Name, DepartmentPath Path, ParentId ParentId, Slug Slug);