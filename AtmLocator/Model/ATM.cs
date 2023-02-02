namespace AtmLocator.Model;

public record ATM(long? id, string name, Location coordinates, string addr, string city, string state, string postalCode, float distance,
    List<string> details, List<string> notes, bool inDoors, Branch branch) 
{

}