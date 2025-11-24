using Mapster;
using MediatR;
using WalletBro.UseCases.Contracts.Persistence;
using WalletBro.UseCases.Invoice.ProcessInvoice;

namespace WalletBro.UseCases.User.Register;

public class RegisterUserHandler(IUserRepository userRepository) : IRequestHandler<RegisterUserCommand, RegisterUserResult> 
{
    public async Task<RegisterUserResult> Handle(RegisterUserCommand userCommand, CancellationToken cancellationToken)
    {
        var response = new RegisterUserResult()
        {
            IsSuccess = true,
            ErrorMessages = []
        };
        var existingUser = await userRepository.GetByEmailAsync(userCommand.Email);

        if (existingUser != null)
        {
            response.IsSuccess = false;
            response.ErrorMessages = ["Email already exists"];
            return response;
        }
        
        var user = userCommand.Adapt<Core.Entities.User>();
        
        user.Id =  Guid.NewGuid();
        user.PasswordHash =  BCrypt.Net.BCrypt.HashPassword(userCommand.Password);
        
        await userRepository.AddAsync(user);
        response.IsSuccess = true;
        return response;
    }
}