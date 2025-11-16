namespace WalletBro.UseCases.Contracts.External.DTOs;

public class ProcessInvoiceResponse
{
    public bool IsSuccess { get; set; } = false;

    public InvoiceData InvoiceData { get; set; } = new();
}

public class InvoiceData
{
    public decimal TotalAmount { get; init; }

    public List<ExpenseDetail> Expenses { get; init; } = [];
}

public class ExpenseDetail
{
    public string Name { get; set; } = string.Empty;

    public decimal Quantity { get; set; }

    public decimal UnitPrice { get; set; }

    public string UnitType { get; set; } = string.Empty;
}