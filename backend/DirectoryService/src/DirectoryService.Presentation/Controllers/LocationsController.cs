using DirectoryService.Application.Locations;
using DirectoryService.Contracts.Location;
using DirectoryService.Domain;
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
    public async Task<IActionResult> GetLocationsAsync(CancellationToken cancellationToken)
    {
        var locations = await _locationsService.ListLocationsAsync(cancellationToken);
        return Ok(locations);
    }

    [HttpGet("{id::guid}")]
    public async Task<IActionResult> GetLocationByIdAsync(
        [FromRoute] Guid id,
        CancellationToken cancellationToken)
    {
        var location = await _locationsService.GetByidAsync(id, cancellationToken);
        // Logic to retrieve a specific location by ID would go here
        if (location == null)
        {
            return NotFound();
        }

        return Ok(location);
    }

    [HttpPost]
    public async Task<Guid> CreateLocationAsync(
        [FromBody] CreateLocationDto locationDto,
        CancellationToken cancellationToken)
    {
        return await _locationsService.CreateAsync(locationDto, cancellationToken);
    }

    [HttpPut("{id::guid}")]
    public async Task<IActionResult> UpdateLocationAsync(
        [FromRoute] Guid id,
        [FromBody] UpdateLocationDto locationDto,
        CancellationToken cancellationToken)
    {
        var result = await _locationsService.UpdateAsync(id, locationDto, cancellationToken);
        if (!result)
        {
            return BadRequest("Failed to update location.");
        }
        else
        {
            return Ok(new { Message = $"Location with ID {id} updated" });
        }
    }

    [HttpDelete("{id::guid}")]
    public async Task<IActionResult> DeleteLocationAsync(
        [FromRoute] Guid id,
        CancellationToken cancellationToken)
    {
        var result = await _locationsService.DeleteAsync(id, cancellationToken);

        if (!result)
        {
            return BadRequest("Failed to delete location.");
        }
        else
        {
            // Logic to delete a specific location by ID would go here
            return Ok(new { Message = $"Location with ID {id} deleted" });
        }
    }
}