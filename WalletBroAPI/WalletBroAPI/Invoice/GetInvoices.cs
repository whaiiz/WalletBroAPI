using FastEndpoints;
using Mapster;
using MediatR;
using WalletBro.UseCases.Invoice.GetInvoices;
using WalletBroAPI.Common;
using WalletBroAPI.Dtos.Invoice;

namespace WalletBroAPI.Invoice;

public class GetInvoices(IMediator mediator) : EndpointWithoutRequest<ApiResponse<GetInvoicesResponse>>
{
    public override void Configure()
    {
        Get("/invoices");
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var command = new GetInvoicesQuery();
        var invoice = await mediator.Send(command, ct);
        var response = new GetInvoicesResponse()
        {
            Invoices =  invoice.Invoices.Adapt<List<InvoiceDto>>()
        };
        
        var successResponse = ApiResponse<GetInvoicesResponse>.Success(
            message: string.Empty,
            data: response
        );
            
        await Send.OkAsync(successResponse, cancellation: ct);
    }
}