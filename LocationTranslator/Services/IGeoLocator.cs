using System.Text;
using LocationTranslator.Model;

namespace LocationTranslator.Services;

public interface IGeoLocator 
{
    public Task<Location> TranslateToLocation(String address, String city, String state, String postalCode);
}

public class GeocodIODemoLocator : IGeoLocator
{
    private const string BaseUrl = "https://api.geocod.io/v1.7/geocode";

    public GeocodIODemoLocator()
    {
    }

    public Task<Location> TranslateToLocation(string address, string city, string state, string postalCode)
    {
        
        var builder = new StringBuilder(address);
        builder.Append(' ');
        if (!string.IsNullOrEmpty(postalCode))
        {
            builder.Append(postalCode);
        }
        else
        {
            builder.Append($"{city}, {state}");
        }

        var httpClient = new HttpClient()
        {
            BaseAddress = new Uri(BaseUrl)
        };
        
        httpClient.GetFromJsonAsync<Location>("",)
    }
}