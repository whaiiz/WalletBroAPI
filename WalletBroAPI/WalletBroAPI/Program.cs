using System.Data;
using FastEndpoints;
using FastEndpoints.Swagger;
using Microsoft.Data.SqlClient;
using WalletBro.Infrastructure.External.InvoiceProcessors.Gemini;
using WalletBro.Infrastructure.External.InvoiceProcessors.Gemini.Config;
using WalletBro.Infrastructure.Persistence.Dapper.Repositories;
using WalletBro.UseCases.Contracts.External;
using WalletBro.UseCases.Contracts.Persistence;
using WalletBro.UseCases.Invoice.Process;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddFastEndpoints();
builder.Services.SwaggerDocument(options =>
{
    options.DocumentSettings = s => { s.Title = "WalletBro API"; s.Version = "v1"; };
});


#region Authentication
var jwtSettings = builder.Configuration.GetSection("JwtSettings");
var key = Encoding.UTF8.GetBytes(jwtSettings["SecretKey"]);

builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(x =>
{
    x.RequireHttpsMetadata = true;
    x.SaveToken = true;
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidIssuer = jwtSettings["Issuer"],
        ValidAudience = jwtSettings["Audience"]
    };
});

builder.Services.AddAuthorization();
#endregion

#region Packages configs
var config = TypeAdapterConfig.GlobalSettings;

config.Scan(
    typeof(WebApiMaspterConfig).Assembly,
    typeof(UseCasesMapsterConfig).Assembly
);

builder.Services.AddScoped<IMapper, ServiceMapper>();
builder.Services.AddSingleton(config);
builder.Services.AddFastEndpoints();
builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssemblyContaining<ProcessInvoiceCommandHandler>());

// Add settings
builder.Services.Configure<GeminiApiSettings>(
    builder.Configuration.GetSection("GeminiApiSettings"));

builder.Services.AddScoped<IDbConnection>(sp =>
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    return new SqlConnection(connectionString);
});

builder.Services.Configure<GeminiApiSettings>(
    builder.Configuration.GetSection("GeminiApiSettings"));

builder.Services.Configure<JwtSettings>(
    builder.Configuration.GetSection("JwtSettings"));
#endregion

#region Dependency Injection
builder.Services.AddScoped<IInvoiceRepository, InvoiceRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IProcessInvoice, ProcessInvoiceGemini>();
builder.Services.AddScoped<ITokenService, JwtTokenService>();

builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<ICurrentUserService, CurrentUserService>();
#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwaggerGen();
    app.UseSwaggerUi(s => s.ConfigureDefaults()); 
}
app.UseHttpsRedirection();

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseAuthentication();  
app.UseAuthorization();

app.UseFastEndpoints(options =>
{
    options.Errors.ResponseBuilder = (failures, ctx, statusCode) =>
    {
        var errors = failures.Select(f =>
            new ErrorDetail(
                Field: f.PropertyName,
                Message: f.ErrorMessage
            )
        );

        return ApiResponse<string>.Error(
            message: "Validation error.",
            errors: errors
        );
    };
});

app.Run();