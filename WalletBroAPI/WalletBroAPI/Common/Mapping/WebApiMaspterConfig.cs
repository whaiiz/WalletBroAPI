using Mapster;
using WalletBro.Core.Entities;
using WalletBro.UseCases.User.Login;
using WalletBro.UseCases.User.Register;
using WalletBroAPI.Dtos.Invoice;
using WalletBroAPI.User;

namespace WalletBroAPI.Common.Mapping;

public class WebApiMaspterConfig : IRegister 
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<RegisterRequest, RegisterUserCommand>();
        config.NewConfig<LoginRequest, LoginCommand>();
        config.NewConfig<WalletBro.Core.Entities.Invoice, InvoiceDto>();
        config.NewConfig<ExpenseDetail, ExpenseDetailDto>();
    }
}