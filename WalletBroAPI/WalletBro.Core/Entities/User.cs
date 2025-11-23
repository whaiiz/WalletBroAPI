namespace WalletBro.Core.Entities;

public class User
{
    public required Guid Id { get; set; }
    
    public required string FirstName { get; init; }
    
    public string LastName { get; init; } = string.Empty;
    
    public required string Email { get; init; }
    
    public required string PasswordHash { get; set; }
    
    public required DateTime DateOfBirth { get; init; }
    
    public DateTime CreatedAt { get; init; } = DateTime.Now;
}