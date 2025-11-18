namespace WalletBroAPI.Common;

public record ApiResponse<T>(
    bool IsSuccess,
    string Message,
    T? Data = default,
    IEnumerable<ErrorDetail>? Errors = null
)
{
    public static ApiResponse<T> Success(string message, T data) =>
        new(true, message, data, null);

    public static ApiResponse<T> Error(string message, IEnumerable<ErrorDetail>? errors = null) =>
        new(false, message, default, errors);
}

public record ErrorDetail(string Field, string Message);