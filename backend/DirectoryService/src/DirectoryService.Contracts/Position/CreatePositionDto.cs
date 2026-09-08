using DirectoryService.Domain.PositionVO;

namespace DirectoryService.Contracts.Position;

sealed public record CreatePositionDto(PositionName PositionName);