using MediatR;

namespace WalletBro.UseCases.User.Login;

// ReSharper disable ClassNeverInstantiated.Global
public class LoginCommand : IRequest<LoginResult>
{
    public required string Email { get; init; }

    public required string Password { get; init; }    
}