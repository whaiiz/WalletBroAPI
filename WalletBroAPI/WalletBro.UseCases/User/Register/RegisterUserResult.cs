namespace WalletBro.UseCases.User.Register;

public class RegisterUserResult
{
    public bool IsSuccess { get; set; }

    public string[] ErrorMessages { get; set; } = [];
}