using MediatR;

namespace WalletBro.UseCases.User.Register;

// ReSharper disable ClassNeverInstantiated.Global
public class RegisterUserCommand() : IRequest<RegisterUserResult>
{
    public required string FirstName { get; set; }
    
    public string LastName { get; set; } = string.Empty;
    
    public required string Email { get; set; }
    
    public required string Password { get; set; }
    
    public required DateTime DateOfBirth { get; set; }
}