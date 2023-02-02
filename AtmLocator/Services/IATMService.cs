using AtmLocator.Model;

namespace AtmLocator.Services;

public interface IATMService
{
    Task<IEnumerable<ATM>> Search(string address, string city, string state, string postalCode, int radius);
	
    Task<IEnumerable<ATM>> Search(float longitude, float latitude, int radius);
}