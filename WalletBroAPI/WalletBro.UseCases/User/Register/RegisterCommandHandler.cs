using Mapster;
using MediatR;
using WalletBro.UseCases.Contracts.Persistence;
using WalletBro.UseCases.Invoice.Process;

namespace WalletBro.UseCases.User.Register;

public class RegisterCommandHandler(IUserRepository userRepository) : IRequestHandler<RegisterCommand, RegisterResult> 
{
    public async Task<RegisterResult> Handle(RegisterCommand command, CancellationToken cancellationToken)
    {
        var response = new RegisterResult()
        {
            IsSuccess = true,
            ErrorMessages = []
        };
        var existingUser = await userRepository.GetByEmailAsync(command.Email);

        if (existingUser != null)
        {
            response.IsSuccess = false;
            response.ErrorMessages = new[] { "Email already exists" };
            return response;
        }
        
        var user = command.Adapt<Core.Entities.User>();
        
        user.Id =  Guid.NewGuid();
        user.PasswordHash =  BCrypt.Net.BCrypt.HashPassword(command.Password);
        
        await userRepository.AddAsync(user);
        response.IsSuccess = true;
        return response;
    }
}