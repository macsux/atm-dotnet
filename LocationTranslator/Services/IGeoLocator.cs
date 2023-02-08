using LocationTranslator.Model;

namespace LocationTranslator.Services;

public interface IGeoLocator 
{
    public Task<Location?> TranslateToLocation(String address, String city, String state, String postalCode);
}