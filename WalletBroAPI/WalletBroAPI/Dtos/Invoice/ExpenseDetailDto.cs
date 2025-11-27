using WalletBro.Core.Common;

namespace WalletBroAPI.Dtos.Invoice;

// ReSharper disable ClassNeverInstantiated.Global
public class ExpenseDetailDto
{
    public string Name { get; set; } = string.Empty;

    public decimal UnitPrice { get; set; }
    
    public UnitType UnitType { get; set; } = UnitType.Unit; 
}