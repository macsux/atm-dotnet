using System.Net;
using AtmLocator.Model;
using AtmLocator.Services;
using Microsoft.AspNetCore.Mvc;

namespace AtmLocator.Controllers;

[ApiController]
[Route("[controller]")]
public class AtmSearchController : ControllerBase
{
    private readonly IATMService _atmSvc;
    private readonly ILogger<AtmSearchController> _log;

    public AtmSearchController(IATMService atmSvc, ILogger<AtmSearchController> log)
    {
        _atmSvc = atmSvc;
        _log = log;
    }

    [HttpGet(Name = "/atmsearch")]
    public async Task<ActionResult<ATM>> Get(string? address, string? city, string? postalCode, string? state, float? latitude, float? longitude, int radius)
    {
        if (!IsValidQueryParams(city, state, postalCode, latitude, longitude))
        {
            _log.LogError("Missing required search parameter");
            return BadRequest();
        }

        try
        {
            var searchResult = IsValidCordinates(latitude, longitude) ? await _atmSvc.Search(latitude.GetValueOrDefault(), longitude.GetValueOrDefault(), radius) : await _atmSvc.Search(address!, city!, state!, postalCode!, radius);
            return Ok(searchResult);
        }
        catch (Exception e)
        {
            throw new Exception("Error search for ATM locations search", e);
        }
    }
    
    protected bool IsValidQueryParams(string? city, string? state, string? postalCode, float? latitude, float? longitude)
    {
        return (IsValidCityState(city, state) || IsValidPostalCode(postalCode) || IsValidCordinates(latitude, longitude));
    }
	
    protected bool IsValidCityState(string? city, string? state) => !(string.IsNullOrEmpty(city) && !string.IsNullOrEmpty(state));

	
    protected bool IsValidPostalCode(string? postalCode)
    {
        return !string.IsNullOrEmpty(postalCode);
    }
	
    protected bool IsValidCordinates(float? latitude, float? longitude)
    {
        return (longitude < 180.0 && longitude > -180.0 && latitude < 90.0 && latitude > -90);
    }
}
