using MediatR;

namespace WalletBro.UseCases.Invoice.GetInvoiceById;

public class GetInvoiceByIdQuery() : IRequest<GetInvoiceByIdResult>
{
    public string Id { get; init; } = string.Empty;
}