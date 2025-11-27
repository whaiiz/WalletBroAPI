// ReSharper disable SqlNoDataSourceInspection

using System.Data;
using Dapper;
using WalletBro.Core.Entities;
using WalletBro.Core.Exceptions;
using WalletBro.UseCases.Contracts.Persistence;

namespace WalletBro.Infrastructure.Persistence.Dapper.Repositories;

// ReSharper disable SqlResolve
public class InvoiceRepository(IDbConnection db) : IInvoiceRepository
{
    public async Task<string> AddAsync(Invoice invoice)
    {
        const string sqlInvoice = """
          INSERT INTO Invoices (Id, UserId, CreatedAt)
          VALUES (@Id, @UserId, @CreatedAt);
        """;

        const string sqlExpense = """
          INSERT INTO ExpenseDetail (Name, UnitPrice, UnitType, InvoiceId, CreatedAt)
          VALUES (@Name, @UnitPrice, @UnitType, @InvoiceId, @CreatedAt);
        """;
        
        if (db.State != ConnectionState.Open)
            db.Open();
        
        using var transaction = db.BeginTransaction();

        try
        {
            var invoiceId = Guid.NewGuid();
            invoice.Id = invoiceId;

            await db.ExecuteAsync(sqlInvoice, invoice, transaction);

            foreach (var expense in invoice.Expenses)
            {
                expense.InvoiceId = invoiceId;
                await db.ExecuteAsync(sqlExpense, expense, transaction);
            }

            transaction.Commit();
            return invoiceId.ToString();
        }
        catch
        {
            throw;
        }
    }

    public async Task<Invoice> GetByIdAsync(string id, Guid userId)
    {
        const string sql = """
           
                       SELECT 
                           i.Id, i.UserId, i.CreatedAt,
                           e.Id AS ExpenseId, e.Name, e.UnitPrice, e.UnitType, e.InvoiceId, e.CreatedAt
                       FROM Invoices i
                       LEFT JOIN ExpenseDetail e ON i.Id = e.InvoiceId
                       WHERE i.Id = @Id AND i.UserId = @UserId;
                   
           """;

        var invoiceDictionary = new Dictionary<Guid, Invoice>();

        await db.QueryAsync<Invoice, ExpenseDetail, Invoice>(
            sql,
            (invoice, expense) =>
            {
                if (!invoiceDictionary.TryGetValue(invoice.Id, out var invoiceEntry))
                {
                    invoiceEntry = invoice;
                    invoiceEntry.Expenses = [];
                    invoiceDictionary.Add(invoice.Id, invoiceEntry);
                }

                invoiceEntry.Expenses.Add(expense);

                return invoiceEntry;
            },
            new { Id = id, UserId = userId },
            splitOn: "ExpenseId"
        );

        return invoiceDictionary.Values.FirstOrDefault() 
               ?? throw new InvoiceNotFoundException(id);
    }

    public async Task<IEnumerable<Invoice>> GetAllAsync(Guid userId)
    {
        const string sql = """
               SELECT 
                   i.Id, i.UserId, i.CreatedAt,
                   e.Id AS ExpenseId, e.Name, e.UnitPrice, e.UnitType, e.InvoiceId, e.CreatedAt
               FROM Invoices i
               LEFT JOIN ExpenseDetail e ON i.Id = e.InvoiceId
               WHERE i.UserId = @UserId
               ORDER BY i.CreatedAt DESC;
           
    """;

        var invoiceDictionary = new Dictionary<Guid, Invoice>();

        await db.QueryAsync<Invoice, ExpenseDetail, Invoice>(
            sql,
            (invoice, expense) =>
            {
                if (!invoiceDictionary.TryGetValue(invoice.Id, out var invoiceEntry))
                {
                    invoiceEntry = invoice;
                    invoiceEntry.Expenses = [];
                    invoiceDictionary.Add(invoice.Id, invoiceEntry);
                }

                invoiceEntry.Expenses.Add(expense);

                return invoiceEntry;
            },
            new { UserId = userId },
            splitOn: "ExpenseId"
        );

        return invoiceDictionary.Values;
    }
}