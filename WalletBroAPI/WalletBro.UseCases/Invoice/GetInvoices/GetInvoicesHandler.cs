using MediatR;
using WalletBro.UseCases.Contracts.Authentication;
using WalletBro.UseCases.Contracts.Persistence;

namespace WalletBro.UseCases.Invoice.GetInvoices;

public class GetInvoicesHandler(ICurrentUserService currentUserService, IInvoiceRepository invoiceRepository) : 
    IRequestHandler<GetInvoicesQuery, GetInvoicesResult>
{
    public async Task<GetInvoicesResult> Handle(GetInvoicesQuery request, CancellationToken cancellationToken)
    {
        var result = new GetInvoicesResult();
        var userId = currentUserService.UserId;

        if (userId == Guid.Empty) return result;

        var invoices = await invoiceRepository.GetAllAsync(userId);

        result.Invoices = invoices.ToList();
        return result;
    }
}