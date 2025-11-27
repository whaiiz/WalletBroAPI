namespace WalletBro.UseCases.User.Login;

public class LoginResult
{
    public bool IsSuccess { get; set; }
    
    public string? Token { get; set; }
    
    public string[] ErrorMessages { get; set; } = [];
}