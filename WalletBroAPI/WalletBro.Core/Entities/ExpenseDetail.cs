using WalletBro.Core.Common;

namespace WalletBro.Core.Entities;

public class ExpenseDetail
{
    public Guid Id { get; init; }

    public string Name { get; init; } = string.Empty;

    public decimal UnitPrice { get; init; }
    
    public UnitType UnitType { get; init; } = UnitType.Unit;

    public Guid InvoiceId { get; set; }
    
    public Invoice? Invoice { get; init; }

    public DateTime CreatedAt { get; init; } = DateTime.Now;
}