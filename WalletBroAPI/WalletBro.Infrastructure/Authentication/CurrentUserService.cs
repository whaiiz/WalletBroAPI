using WalletBro.UseCases.Contracts.Authentication;
using Microsoft.AspNetCore.Http;

namespace WalletBro.Infrastructure.Authentication
{
    public class CurrentUserService : ICurrentUserService
    {
        public Guid UserId { get; }

        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            var user = httpContextAccessor.HttpContext?.User;
            var userIdClaim = user?.Claims
                .FirstOrDefault(c => c.Type == "name")
                ?.Value;
            
            UserId = !string.IsNullOrEmpty(userIdClaim) && Guid.TryParse(userIdClaim, out var parsedGuid)
                ? parsedGuid
                : Guid.Empty;
        }
    }
}