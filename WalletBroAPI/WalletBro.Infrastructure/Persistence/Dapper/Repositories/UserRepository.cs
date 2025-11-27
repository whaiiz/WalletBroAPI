using System.Data;
using Dapper;
using WalletBro.Core.Entities;
using WalletBro.UseCases.Contracts.Persistence;

namespace WalletBro.Infrastructure.Persistence.Dapper.Repositories;

public class UserRepository(IDbConnection db) : IUserRepository
{
    public async Task<User?> GetByEmailAsync(string email)
    {
        const string sql = @"
            SELECT Id, FirstName, LastName, Email, PasswordHash, DateOfBirth, CreatedAt
            FROM Users
            WHERE Email = @Email;
        ";
        
        return await db.QueryFirstOrDefaultAsync<User>(sql, new { Email = email });
    }

    public async Task<User?> GetById(string id)
    {
        const string sql = @"
            SELECT Id, FirstName, LastName, Email, PasswordHash, DateOfBirth, CreatedAt
            FROM Users
            WHERE Id = @Id;
        ";
        
        return await db.QueryFirstOrDefaultAsync<User>(sql, new { Id = id });
    }

    public async Task<bool> AddAsync(User user)
    {
        const string sql = @"
            INSERT INTO Users (Id, FirstName, LastName, Email, PasswordHash, DateOfBirth, CreatedAt)
            VALUES (@Id, @FirstName, @LastName, @Email, @PasswordHash, @DateOfBirth, @CreatedAt);
        ";
            
        var rows = await db.ExecuteAsync(sql, user);
        return rows > 0;
    }
}