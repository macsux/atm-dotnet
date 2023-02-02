using AtmLocator.Exchange;
using AtmLocator.Model;

namespace AtmLocator.Services.Impl;

public class DefaultAtmService : IATMService
{
    private readonly ILocTranslationService _transService;

    private readonly IATMClient _atmClient;

    private readonly IBranchClient _branchClient;

    public DefaultAtmService(IBranchClient branchClient, IATMClient atmClient, ILocTranslationService transService)
    {
        _branchClient = branchClient;
        _atmClient = atmClient;
        _transService = transService;
    }


    public async Task<IEnumerable<ATM>> Search(string address, string city, string state, string postalCode, int radius)
    {
        var location = string.IsNullOrEmpty(postalCode) ?  await _transService.translateLoc(address, postalCode) : await _transService.translateLoc(address, city, state);

        return await Search(location.latitude, location.longitude, radius);
    }
	
    public async Task<IEnumerable<ATM>> Search(float latitude, float longitude, int radius) 
    {
        var searchResults = await _atmClient.Search(latitude, longitude, radius, false);
        return await Task.WhenAll(searchResults.Select(async atm => atm.BranchId == null ? ResultToModel(atm, null) : await GetBranch(atm)));

    }

    private async Task<ATM> GetBranch(ATMSearchResult res)
    {
        var branch = await _branchClient.FindById(res.BranchId!.Value);
        return ResultToModel(res, branch);
    }

    private ATM ResultToModel(ATMSearchResult atmRes, Branch? branch)
    {		
        var atmLoc = new Location(atmRes.Latitude, atmRes.Longitude);
		
        return new ATM(atmRes.Id, atmRes.Name, atmLoc, atmRes.Addr, atmRes.City, atmRes.State, atmRes.PostalCode, atmRes.Distance, atmRes.Details, atmRes.Notes, atmRes.InDoors, branch);
    }
}