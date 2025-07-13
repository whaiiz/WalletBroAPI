using FastEndpoints;
using Microsoft.AspNetCore.Http.HttpResults;

namespace WalletBroAPI.Invoice;

public class Process : Endpoint<ProcessInvoiceRequest,
    Results<
        Ok<ProcessInvoiceResponse>,
        BadRequest,
        ProblemDetails>>
{
    public override void Configure()
    {
        Post("/invoice/process");
    }

    public override async Task HandleAsync(ProcessInvoiceRequest req, CancellationToken ct)
    {
        throw new NotImplementedException();
    }
}