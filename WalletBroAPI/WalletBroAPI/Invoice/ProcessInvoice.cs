using FastEndpoints;
using MediatR;
using WalletBro.UseCases.Invoice.ProcessInvoice;
using WalletBroAPI.Common;

namespace WalletBroAPI.Invoice;

public class ProcessInvoice(IMediator mediator) : Endpoint<ProcessRequest, ApiResponse<ProcessResponse>>
{
    public override void Configure()
    {
        Post("/invoices/process");
    }

    public override async Task HandleAsync(ProcessRequest req, CancellationToken ct)
    {
        var command = new ProcessInvoiceCommand
        {
            FileName = req.FileName,
            ContentType = req.ContentType,
            Base64Content = req.Base64Content
        };

        var result = await mediator.Send(command, ct);

        if (!result.IsSuccess)
        {
            var errors = result.ErrorMessages?.Select(e => new ErrorDetail("", e));

            var errorResponse = ApiResponse<ProcessResponse>.Error(
                message: "Invoice Processing failed",
                errors: errors
            );

            await Send.ResponseAsync(errorResponse, statusCode: StatusCodes.Status400BadRequest, cancellation: ct);
            return;
        }
        
        var payload = new ProcessResponse();
        var successResponse = ApiResponse<ProcessResponse>.Success(
            message: "Process invoice successful",
            data: payload
        );
        
        await Send.OkAsync(successResponse, cancellation: ct);
    }
}