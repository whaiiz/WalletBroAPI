using FastEndpoints;
using Mapster;
using MediatR;
using WalletBro.UseCases.User.Login;
using WalletBroAPI.Common;

namespace WalletBroAPI.User;

public class Login(IMediator mediator) : Endpoint<LoginRequest, ApiResponse<LoginResponse>>
{
    public override void Configure()
    {
        Post("/user/login");
        AllowAnonymous();
        
        Summary(s =>
        {
            s.Summary = "Authenticate a user.";
            s.Description = "Validates credentials and returns a JWT if successful.";
        });
    }

    public override async Task HandleAsync(LoginRequest req, CancellationToken ct)
    {
        var login = req.Adapt<LoginCommand>();
        var result = await mediator.Send(login, ct);

        if (!result.IsSuccess)
        {
            var errors = result.ErrorMessages?.Select(e => new ErrorDetail("", e));

            var errorResponse = ApiResponse<LoginResponse>.Error(
                message: "Invalid login credentials",
                errors: errors
            );

            await Send.ResponseAsync(errorResponse, statusCode: StatusCodes.Status400BadRequest, cancellation: ct);
            return;
        }

        var payload = new LoginResponse();
        payload.Token = result.Token;
        
        var successResponse = ApiResponse<LoginResponse>.Success(
            message: "Login successful",
            data: payload
        );
        
        await Send.OkAsync(successResponse, cancellation: ct);
    }
}