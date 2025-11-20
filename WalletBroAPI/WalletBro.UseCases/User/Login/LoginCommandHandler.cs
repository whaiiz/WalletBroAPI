using MediatR;
using WalletBro.UseCases.Contracts.Authentication;
using WalletBro.UseCases.Contracts.Persistence;

namespace WalletBro.UseCases.User.Login;

public class LoginCommandHandler(IUserRepository repository, ITokenService tokenService) : 
    IRequestHandler<LoginCommand, LoginResult>
{
    public async Task<LoginResult> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var response = new LoginResult();
        var user = await repository.GetByEmailAsync(request.Email);

        if (user == null)
        {
            response.ErrorMessages = ["Email not found"];
            return response;
        }
        
        var isValid = BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash);

        if (!isValid)
        {
            response.ErrorMessages = ["Login failed"];
            return response;
        }
        
        response.IsSuccess = true;
        response.Token = tokenService.GenerateToken(user.Email);
        
        return response;
    }
}