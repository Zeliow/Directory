using DirectoryService.Contracts.Location;
using Microsoft.AspNetCore.Mvc;

namespace DirectoryService.Presentation.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LocationsController : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetLocations()
    {
        // Logic to retrieve locations would go here
        return Ok(Array.Empty<string>());
    }

    [HttpGet("id")]
    public async Task<IActionResult> GetLocationById(int id)
    {
        // Logic to retrieve a specific location by ID would go here
        return NotFound();
    }

    [HttpPost]
    public async Task<IActionResult> CreateLocation([FromBody] CreateLocationDto locationDto)
    {
        // Logic to create a new location would go here
        return Ok(new { Message = "Department created" });
    }

    [HttpPut("id")]
    public async Task<IActionResult> UpdateLocation(int id, [FromBody] UpdateLocationDto locationDto)
    {
        // Logic to update a specific location by ID would go here
        return Ok(new { Message = $"Location with ID {id} updated" });
    }

    [HttpDelete("id")]
    public async Task<IActionResult> DeleteLocation(int id)
    {
        // Logic to delete a specific location by ID would go here
        return Ok(new { Message = $"Location with ID {id} deleted" });
    }
}