using System.ComponentModel.DataAnnotations.Schema;

namespace Atm.Entity;

public class ATM
{
    public int Id { get; set; }
    public string Name { get; set; }
    public float Latitude { get; set; }
    public float Longitude { get; set; }
    public string Addr { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public string PostalCode { get; set; }
    public bool InDoors { get; set; }
    public long? BranchId { get; set; }
}

public class ATMDetail
{
    public long Id { get; set; }
    public string Detail { get; set; }
    public long AtmId { get; set; }
    public ATM Atm { get; set; }
}

public class ATMNote
{
    public long Id { get; set; }
    public string Note { get; set; }
    public long AtmId { get; set; }
}