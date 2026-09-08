using DirectoryService.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace DirectoryService.Application.Interfaces;

public interface ILocationRepository
{
    Task<Guid> AddAsync(Location location, CancellationToken cancellationToken);
}