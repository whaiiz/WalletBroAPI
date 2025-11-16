using System.Runtime.Intrinsics.X86;
using MediatR;
using WalletBro.UseCases.Contracts.External;
using WalletBro.UseCases.Contracts.External.DTOs;
using WalletBro.UseCases.Contracts.Persistence;
using ExpenseDetail = WalletBro.Core.Entities.ExpenseDetail;

namespace WalletBro.UseCases.Invoice.Process;

public class ProcessInvoiceCommandHandler(IProcessInvoice processInvoice, IInvoiceRepository invoiceRepository)
    : IRequestHandler<ProcessInvoiceCommand, ProcessInvoiceResult>
{
    public async Task<ProcessInvoiceResult> Handle(ProcessInvoiceCommand request, CancellationToken cancellationToken)
    {
        var result = new ProcessInvoiceResult();
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
            UserId = 2,
            Expenses = processInvoiceResult.InvoiceData.Expenses
                .Select(x => new ExpenseDetail
                {
                    Name = x.Name,
                    UnitPrice = x.UnitPrice,
                    UnitType = x.UnitType,
                    CreatedAt = DateTime.Now
                }).ToList()
        };
        
        var addInvoiceResult = await invoiceRepository.AddAsync(invoice);
        result.IsSuccess = addInvoiceResult != 0;
        
        return result;
    }
}