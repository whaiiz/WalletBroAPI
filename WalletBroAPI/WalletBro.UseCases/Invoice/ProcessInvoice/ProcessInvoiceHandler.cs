using System.Runtime.Intrinsics.X86;
using MediatR;
using WalletBro.Core.Common;
using WalletBro.UseCases.Contracts.Authentication;
using WalletBro.UseCases.Contracts.External;
using WalletBro.UseCases.Contracts.External.DTOs;
using WalletBro.UseCases.Contracts.Persistence;
using ExpenseDetail = WalletBro.Core.Entities.ExpenseDetail;

namespace WalletBro.UseCases.Invoice.ProcessInvoice;

public class ProcessInvoiceHandler(IProcessInvoice processInvoice, 
    IInvoiceRepository invoiceRepository,
    ICurrentUserService currentUserService)
    : IRequestHandler<ProcessInvoiceCommand, ProcessInvoiceResult>
{
    public async Task<ProcessInvoiceResult> Handle(ProcessInvoiceCommand request, CancellationToken cancellationToken)
    {
        var result = new ProcessInvoiceResult();

        if (currentUserService.UserId == Guid.Empty) return result;
        
        var reqProcessInvoiceExternal = new ProcessInvoiceRequest()
        {
            FileName = request.FileName,
            ContentType = request.ContentType,
            Base64Content = request.Base64Content
        };
        var processInvoiceResult = await processInvoice.ProcessInvoiceAsync(reqProcessInvoiceExternal);

        if (!processInvoiceResult.IsSuccess) return result;
        
        var invoice = new Core.Entities.Invoice
        {
            UserId = currentUserService.UserId,
            Expenses = processInvoiceResult.InvoiceData.Expenses
                .Select(x => new ExpenseDetail
                {
                    Name = x.Name,
                    UnitPrice = x.UnitPrice,
                    UnitType =  Enum.Parse<UnitType>(x.UnitType, ignoreCase: true),
                    CreatedAt = DateTime.Now
                }).ToList()
        };
        
        var addInvoiceResult = await invoiceRepository.AddAsync(invoice);
        result.IsSuccess = !string.IsNullOrEmpty(addInvoiceResult);
        
        return result;
    }
}