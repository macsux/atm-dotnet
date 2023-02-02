using System.Net;
using AtmLocator.Model;
using AtmLocator.Services;
using Microsoft.AspNetCore.Mvc;

namespace AtmLocator.Controllers;

[ApiController]
[Route("[controller]")]
public class AtmSearch : ControllerBase
{
    private readonly IATMService _atmSvc;
    private readonly ILogger<AtmSearch> _log;

    public AtmSearch(IATMService atmSvc, ILogger<AtmSearch> log)
    {
        _atmSvc = atmSvc;
        _log = log;
    }

    [HttpGet(Name = "GetWeatherForecast")]
    public async Task<ActionResult<ATM>> Get(string address, string city, string postalCode, string state, float latitude, float longitude, int radius)
    {
        if (!IsValidQueryParams(city, state, postalCode, latitude, longitude))
        {
            _log.LogError("Missing required search paramater.");
            return BadRequest();
        }

        try
        {
            var searchResult = IsValidCordinates(latitude, longitude) ? await _atmSvc.Search(latitude, longitude, radius) : await _atmSvc.Search(address, city, state, postalCode, radius);
            return Ok(searchResult);
        }
        catch (Exception e)
        {
            throw new Exception("Error search for ATM locations search", e);
        }
    }
    
    protected bool IsValidQueryParams(string city, string state, string postalCode, float latitude, float longitude)
    {
        return (IsValidCityState(city, state) || IsValidPostalCode(postalCode) || IsValidCordinates(latitude, longitude));
    }
	
    protected bool IsValidCityState(string city, string state)
    {
        return   ((!string.IsNullOrEmpty(city) && !string.IsNullOrEmpty(state)) || 
                 (string.IsNullOrEmpty(city) && !string.IsNullOrEmpty(state)) || 
                 (!string.IsNullOrEmpty(city) && string.IsNullOrEmpty(state))) ? false : true;

    }
	
    protected bool IsValidPostalCode(string postalCode)
    {
        return string.IsNullOrEmpty(postalCode);
    }
	
    protected bool IsValidCordinates(float latitude, float longitude)
    {
        return (longitude < 180.0 && longitude > -180.0 && latitude < 90.0 && latitude > -90);
    }
}
