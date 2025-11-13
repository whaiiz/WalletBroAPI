using System.Data;
using Dapper;
using WalletBro.UseCases.Contracts.Persistence;

namespace WalletBro.Infrastructure.Persistence.Dapper.Repositories;

public class InvoiceRepository(IDbConnection db) : IInvoiceRepository
{
    public async Task<int> AddAsync(Core.Entities.Invoice invoice)
    {
        const string sqlInvoice = @"
            INSERT INTO Invoices (UserId, CreatedAt)
            VALUES (@UserId, @CreatedAt);
            SELECT CAST(SCOPE_IDENTITY() AS int);
        ";

        const string sqlExpense = @"
            INSERT INTO ExpenseDetail (Name, UnitPrice, UnitType, InvoiceId, CreatedAt)
            VALUES (@Name, @UnitPrice, @UnitType, @InvoiceId, @CreatedAt);
        ";

        if (db.State != ConnectionState.Open)
            db.Open();
        
        using var transaction = db.BeginTransaction();

        try
        {
            var invoiceId = await db.ExecuteScalarAsync<int>(
                sqlInvoice, invoice, transaction);

            foreach (var expense in invoice.Expenses)
            {
                expense.InvoiceId = invoiceId;
                await db.ExecuteAsync(sqlExpense, expense, transaction);
            }

            transaction.Commit();
            return invoiceId;
        }
        catch
        {
            transaction.Rollback();
            throw;
        }
    }
}