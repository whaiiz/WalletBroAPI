namespace WalletBro.Core.Entities;

public class Invoice
{
    public int Id { get; set; }

    public Guid UserId { get; set; }

    public User? User { get; set; }
    
    public List<ExpenseDetail> Expenses { get; set; } = new();
    
    public DateTime CreatedAt { get; set; } = DateTime.Now;
}