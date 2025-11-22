using WalletBro.Core.Common;

namespace WalletBro.Core.Entities;

public class ExpenseDetail
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public decimal  UnitPrice { get; set; }
    
    public UnitType UnitType { get; set; } = UnitType.Unit; 
    
    public int InvoiceId { get; set; }
    
    public Invoice?  Invoice { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.Now;
}