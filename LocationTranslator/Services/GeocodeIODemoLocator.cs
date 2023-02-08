using System.Net;
using System.Text;
using LocationTranslator.Model;
using Microsoft.AspNetCore.WebUtilities;

namespace LocationTranslator.Services;

public class GeocodeIODemoLocator : IGeoLocator
{
    private readonly HttpClient _client;
    public const string BaseUrl = "https://api.geocod.io/v1.7/";

    public GeocodeIODemoLocator(HttpClient client)
    {
        _client = client;
    }

    public async Task<Location?> TranslateToLocation(string address, string city, string state, string postalCode)
    {
        
        var addressBuilder = new StringBuilder(address);
        addressBuilder.Append(' ');
        if (!string.IsNullOrEmpty(postalCode))
        {
            addressBuilder.Append(postalCode);
        }
        else
        {
            addressBuilder.Append($"{city}, {state}");
        }

        var url = QueryHelpers.AddQueryString("geocode", new Dictionary<string, string?>
        {
            { "q", addressBuilder.ToString() },
            { "api_key", "DEMO" },
        });
        try
        {
            var results = await _client.GetFromJsonAsync<SearchRes>(url);
            if (!results?.Results.Any() ?? false)
            {
                return null;
            }

            var loc = results!.Results.First().Location;
            return new Location(loc.Lat, loc.Lng);
        }
        catch (HttpRequestException e)
        {
            if (e.StatusCode == HttpStatusCode.UnprocessableEntity) // GeocodIO return 422 if it can't process the information we gave it.  This includes non-existent zip codes.
                return null;
            throw;
        }

    }
    
    private class SearchRes
    {
        public List<Results> Results { get; set; }
    }

    private class Results
    {
        public Loc Location { get; set; }
    }

    private class Loc
    {
        public float Lat { get; set; }
        public float Lng { get; set; }
    }
}