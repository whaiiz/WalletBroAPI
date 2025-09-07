namespace WalletBro.Core.Entities;

public class User
{
    public int Id { get; set; }
    
    public string FirstName { get; set; } = string.Empty;
    
    public string LastName { get; set; } = string.Empty;
    
    public string Email { get; set; } = string.Empty;
    
    public DateTime DateOfBirth { get; set; } = DateTime.MinValue!;
    
    public DateTime CreatedAt { get; set; } = DateTime.Now;
}