using Mapster;
using WalletBro.UseCases.User.Register;

namespace WalletBro.UseCases.Common.Mappings;

public class UseCasesMapsterConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<RegisterCommand, Core.Entities.User>()
            .Map(dest => dest.PasswordHash, src => src.Password);
    }
}