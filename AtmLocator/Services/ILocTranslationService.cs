using AtmLocator.Model;

namespace AtmLocator.Services;

public interface ILocTranslationService 
{
    public Task<Location> translateLoc(string address, string city, string state);
	
    public Task<Location> translateLoc(string address, string postalCode);
}