using FastEndpoints;
using Mapster;
using MediatR;
using WalletBro.Core.Exceptions;
using WalletBro.UseCases.Invoice.GetInvoiceById;
using WalletBroAPI.Common;
using WalletBroAPI.Dtos.Invoice;

namespace WalletBroAPI.Invoice;

public class GetInvoiceByIdById(IMediator mediator) : EndpointWithoutRequest<ApiResponse<GetInvoiceByIdByIdResponse>>
{
    public override void Configure()
    {
        Get("/invoices/{Id}");
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var id = Route<string>("Id");
        var command = new GetInvoiceByIdQuery { Id = id! };

        try
        {
            var invoice = await mediator.Send(command, ct);

            if (invoice.Invoice == null) throw new InvoiceNotFoundException(id!);
            
            var response = new GetInvoiceByIdByIdResponse()
            {
                Invoice =  invoice.Invoice.Adapt<InvoiceDto>()
            };
            
            var successResponse = ApiResponse<GetInvoiceByIdByIdResponse>.Success(
                message: string.Empty,
                data: response
            );
            
            await Send.OkAsync(successResponse, cancellation: ct);
        }
        catch (InvoiceNotFoundException ex)
        {
            var errorResponse = ApiResponse<GetInvoiceByIdByIdResponse>.Error(
                message: "Invoice not found");
            await Send.ResponseAsync(errorResponse, statusCode: StatusCodes.Status400BadRequest, cancellation: ct);
        }
    }
}