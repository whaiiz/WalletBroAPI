using FastEndpoints;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using WalletBro.UseCases.Invoice.Process;

namespace WalletBroAPI.Invoice;

public class Process(IMediator mediator) : Endpoint<ProcessInvoiceRequest,
    Results<
        Ok<ProcessInvoiceResponse>,
        BadRequest,
        ProblemDetails>>
{
    public override void Configure()
    {
        Post("/invoice/process");
        AllowAnonymous();
    }

    public override async Task HandleAsync(ProcessInvoiceRequest req, CancellationToken ct)
    {
        var command = new ProcessInvoiceCommand
        {
            FileName = req.FileName,
            ContentType = req.ContentType,
            Base64Content = req.Base64Content
        };

        var result = await mediator.Send(command, ct);

        if (result.IsSuccess)
        {
            await Send.ResultAsync(TypedResults.Ok<ProcessInvoiceResponse>(new()
            {
                IsSuccess = true
            }));
        }
        else
        {
            await Send.ResultAsync(TypedResults.BadRequest());
        }
    }
}