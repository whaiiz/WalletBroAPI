using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using WalletBro.UseCases.Contracts.Authentication;

namespace WalletBro.Infrastructure.Authentication
{
    public class CurrentUserService : ICurrentUserService
    {
        public string? Email { get; }

        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            var user = httpContextAccessor.HttpContext?.User;
            Email =  user?.Claims
                .FirstOrDefault(c => c.Type == ClaimTypes.Email)
                ?.Value;
        }
    }
}