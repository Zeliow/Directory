using DirectoryService.Application;
using DirectoryService.Contracts.Location;
using Microsoft.AspNetCore.Mvc;

namespace DirectoryService.Presentation.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LocationsController : ControllerBase
{
    private readonly ILocationsService _locationsService;

    public LocationsController(ILocationsService locationsService)
    {
        _locationsService = locationsService;
    }

    [HttpGet]
    public async Task<IActionResult> GetLocations(CancellationToken cancellationToken)
    {
        // Logic to retrieve locations would go here
        return Ok(Array.Empty<string>());
    }

    [HttpGet("{id::guid}")]
    public async Task<IActionResult> GetLocationById(
        [FromRoute] Guid id,
        CancellationToken cancellationToken)
    {
        // Logic to retrieve a specific location by ID would go here
        return NotFound();
    }

    [HttpPost]
    public async Task<Guid> CreateLocation(
        [FromBody] CreateLocationDto locationDto,
        CancellationToken cancellationToken)
    {
        return await _locationsService.CreateAsync(locationDto, cancellationToken);
    }

    [HttpPut("{id::guid}")]
    public async Task<IActionResult> UpdateLocation(
        [FromRoute] Guid id,
        [FromBody] UpdateLocationDto locationDto,
        CancellationToken cancellationToken)
    {
        // Logic to update a specific location by ID would go here
        return Ok(new { Message = $"Location with ID {id} updated" });
    }

    [HttpDelete("{id::guid}")]
    public async Task<IActionResult> DeleteLocation(
        [FromRoute] Guid id,
        CancellationToken cancellationToken)
    {
        // Logic to delete a specific location by ID would go here
        return Ok(new { Message = $"Location with ID {id} deleted" });
    }
}