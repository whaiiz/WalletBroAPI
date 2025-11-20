using MediatR;

namespace WalletBro.UseCases.User.Login;

public class LoginCommand : IRequest<LoginResult>
{
    public string Email { get; set; }

    public string Password { get; set; }    
}