using AtmLocator.Model;

namespace AtmLocator.Exchange;

public interface IBranchClient 
{
    // @GetExchange("/{id}")
    public Task<Branch> FindById(long id);
}