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
builder.Services.AddScoped<IInvoiceRepository, InvoiceRepository>();
builder.Services.AddScoped<IProcessInvoice, ProcessInvoiceGemini>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwaggerGen();
    app.UseSwaggerUi(s => s.ConfigureDefaults()); 
}
app.UseHttpsRedirection();

// app.UseAuthentication();  
// app.UseAuthorization();

app.UseFastEndpoints();

app.Run();