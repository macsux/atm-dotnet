using AtmLocator.Exchange;
using AtmLocator.Model;

namespace AtmLocator.Services.Impl;

public class DefaultLocTranslationService : ILocTranslationService
{
    private readonly ILocTranslationClient _locTransClient;

    public DefaultLocTranslationService(ILocTranslationClient locTransClient)
    {
        _locTransClient = locTransClient;
    }

    public Task<Location> translateLoc(string address, string city, string state)
    {
        return _locTransClient.TranslateLoc(address, city, state, "");
    }

    public Task<Location> translateLoc(string address, string postalCode)
    {
        return _locTransClient.TranslateLoc(address, "", "", postalCode);
    }
}