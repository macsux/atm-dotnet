using AtmLocator.Model;

namespace AtmLocator.Exchange;

public interface ILocTranslationClient 
{
    // @GetExchange("/loc")
    public Task<Location> TranslateLoc(string address,string city, string state, string postalCode);
}