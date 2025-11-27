using MediatR;
using WalletBro.UseCases.Contracts.Authentication;
using WalletBro.UseCases.Contracts.Persistence;

namespace WalletBro.UseCases.Invoice.GetInvoiceById;

public class GetInvoiceByIdHandler(ICurrentUserService currentUserService, 
    IInvoiceRepository invoiceRepository) : 
    IRequestHandler<GetInvoiceByIdQuery, GetInvoiceByIdResult>
{
    public async Task<GetInvoiceByIdResult> Handle(GetInvoiceByIdQuery request, CancellationToken cancellationToken)
    {
        var result = new GetInvoiceByIdResult();
        var userId = currentUserService.UserId;

        if (userId == Guid.Empty) return result;

        var invoice = await invoiceRepository.GetByIdAsync(request.Id, userId);

        result.Invoice = invoice;

        return result;
    }
}