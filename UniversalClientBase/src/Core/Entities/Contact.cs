namespace UniversalClientBase.Core.Entities;

public class Contact
{
    public int Id {get; set;}

    public required string FirstName {get; set;}
    public required string Email {get; set;}
    

    public string? LastName {get; set;}
    public string? Phone {get; set;}

    public string Status {get; set;} = "Prospecto";

    public DateTime CreatedAt {get; set;} = DateTime.UtcNow;
}