using FastEndpoints;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using WalletBro.UseCases.User.Register;
using WalletBroAPI.Common;

namespace WalletBroAPI.User;

public class Register(IMediator mediator) : Endpoint<RegisterRequest, ApiResponse<RegisterResponse>>
{
    public override void Configure()
    {
        Post("/user/register"); 
        AllowAnonymous();
        
        Summary(s =>
        {
            s.Summary = "Registers a user.";
            s.Description = "Validates credentials and returns a JWT if successful.";
        });
    }

    public override async Task HandleAsync(RegisterRequest req, CancellationToken ct)
    {
        var command = req.Adapt<RegisterCommand>();
        var result = await mediator.Send(command, ct);
        
        if (!result.IsSuccess)
        {
            var errors = result.ErrorMessages?.Select(e => new ErrorDetail("", e));

            var errorResponse = ApiResponse<RegisterResponse>.Error(
                message: "Unsuccessful register",
                errors: errors
            );

            await Send.ResponseAsync(errorResponse, statusCode: StatusCodes.Status400BadRequest, cancellation: ct);
            return;
        }
        
        var payload = new RegisterResponse();
        var successResponse = ApiResponse<RegisterResponse>.Success(
            message: "Register successful",
            data: payload
        );
        
        await Send.OkAsync(successResponse, cancellation: ct);
    }
} 
