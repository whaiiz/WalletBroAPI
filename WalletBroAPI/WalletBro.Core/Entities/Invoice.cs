namespace WalletBro.Core.Entities;

public class Invoice
{
    public Guid Id { get; set; }

    public Guid UserId { get; init; }

    public User? User { get; init; }
    
    public List<ExpenseDetail> Expenses { get; set; } = [];
    
    public DateTime CreatedAt { get; init; } = DateTime.Now;
}