namespace AtmLocator.Model;

public record Branch(string name, string addr, string city, string state, string postalCode, float distance, List<BranchHours> hours,
    List<string> details, List<string> notes)
{
}