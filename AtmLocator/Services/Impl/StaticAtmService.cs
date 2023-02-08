using AtmLocator.Model;

namespace AtmLocator.Services.Impl;

public class StaticAtmService : IATMService
{
    public Task<IEnumerable<ATM>> Search(string address, string city, string state, string postalCode, int radius)
    {
        return Search(34.1398f, 118.3506f, radius);
    }

    public Task<IEnumerable<ATM>> Search(float longitude, float latitude, int radius)
    {
        var coordinates = new Location(longitude, latitude);
		
        var atm = new ATM(null, "Test ATM", coordinates, "1313 Equator Lane", "state", "CA",
            "91608", 10.6f, new List<string>{ "Next to It" }, new List<string> { "Run, Don't Walk" }, true, null);
        return Task.FromResult(new[]{atm}.AsEnumerable());
    }
}