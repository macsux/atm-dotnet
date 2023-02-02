namespace AtmLocator.Exchange;

public record ATMSearchResult(long Id, string Name, float Latitude, float Longitude, string Addr, string City, string State, string PostalCode, float Distance,
    List<string> Details, List<string> Notes, bool InDoors, long? BranchId);