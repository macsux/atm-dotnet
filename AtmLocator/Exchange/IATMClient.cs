namespace AtmLocator.Exchange;

public interface IATMClient
{
    // @GetExchange("/atm/locsearch")
    public Task<IEnumerable<ATMSearchResult>> Search(float latitude, float longitude,  int radius, bool branchLocOnly);
}