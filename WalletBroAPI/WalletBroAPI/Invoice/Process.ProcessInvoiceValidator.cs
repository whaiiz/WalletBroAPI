using System.Data;
using FastEndpoints;
using FluentValidation;

namespace WalletBroAPI.Invoice
{
    public class ProcessInvoiceValidator : Validator<ProcessInvoiceRequest>
    {
        public ProcessInvoiceValidator()
        {
            RuleFor(x => x.FileName)
                .NotEmpty()
                .WithMessage("Name is required");
            
            RuleFor(x => x.Base64Content)
                .NotEmpty()
                .WithMessage("Base64Content is required");
            
            RuleFor(x => x.ContentType)
                .NotEmpty()
                .WithMessage("ContentType is required");
        }
    }
}
