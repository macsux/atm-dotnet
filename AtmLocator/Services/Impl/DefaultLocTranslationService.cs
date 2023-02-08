using AtmLocator.Exchange;
using AtmLocator.Model;
using Microsoft.AspNetCore.WebUtilities;

namespace AtmLocator.Services.Impl;

public class DefaultLocTranslationService : ILocTranslationService
{
    private readonly HttpClient _client;

    public DefaultLocTranslationService(HttpClient client)
    {
        _client = client;
    }


    public Task<Location> translateLoc(string address, string city, string state)
    {
        return translateLoc(address, city, state, "");
    }

    public Task<Location> translateLoc(string address, string postalCode)
    {
        return translateLoc(address, "", "", postalCode);
    }

    private async Task<Location> translateLoc(string? address, string? city, string? state, string? postalCode)
    {
        var url = QueryHelpers.AddQueryString("loc", new Dictionary<string, string?>
        {
            { "address", address },
            { "city", city },
            { "state", state },
            { "postalCode", postalCode }
        });
        return await _client.GetFromJsonAsync<Location>(url) ?? throw new Exception("Unable to retrieve location");
    }
 
}