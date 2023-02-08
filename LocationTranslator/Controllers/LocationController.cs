using LocationTranslator.Model;
using LocationTranslator.Services;
using Microsoft.AspNetCore.Mvc;

namespace LocationTranslator.Controllers;

[ApiController]
[Route("[controller]")]
public class LocationController : ControllerBase
{
    private readonly IGeoLocator _geoLoc;
    private readonly ILogger<LocationController> _log;

    public LocationController(IGeoLocator geoLoc, ILogger<LocationController> log)
    {
        _geoLoc = geoLoc;
        _log = log;
    }

    [HttpGet("/loc")]
    public async Task<ActionResult<Location?>> Get(string? address, string? city, string? state, string? postalCode)
    {
        _log.LogInformation("Searching for geo coordinates");
		
        if (!IsValidQueryParams(city, state, postalCode))
        {
            _log.LogError("Missing required search parameter");
            return BadRequest("Missing required search parameter");
        }

        return await _geoLoc.TranslateToLocation(address!, city!, state!, postalCode!);
    }
    
    protected bool IsValidQueryParams(string? city, string? state, string? postalCode) => IsValidCityState(city, state) || IsValidPostalCode(postalCode);

    protected bool IsValidCityState(string? city, string? state) => !(string.IsNullOrEmpty(city) || string.IsNullOrEmpty(state));
	
    protected bool IsValidPostalCode(string postalCode) => !string.IsNullOrEmpty(postalCode);
}