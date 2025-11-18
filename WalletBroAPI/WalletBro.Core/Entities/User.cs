namespace WalletBro.Core.Entities;

public class User
{
    public required Guid Id { get; set; }
    
    public required string FirstName { get; set; }
    
    public string LastName { get; set; } = string.Empty;
    
    public required string Email { get; set; }
    
    public required string PasswordHash { get; set; }
    
    public required DateTime DateOfBirth { get; set; }
    
    public DateTime CreatedAt { get; set; } = DateTime.Now;
}