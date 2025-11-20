using Mapster;
using WalletBro.UseCases.User.Login;
using WalletBro.UseCases.User.Register;
using WalletBroAPI.User;

namespace WalletBroAPI.Common.Mapping;

public class WebApiMaspterConfig : IRegister 
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<RegisterRequest, RegisterCommand>();
        config.NewConfig<LoginRequest, LoginCommand>();
    }
}