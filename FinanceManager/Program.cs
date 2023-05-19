using FinanceManager.Library.Interfaces;
using FinanceManager.Services;
using Microsoft.EntityFrameworkCore;
using FinanceManager.DAL;

var builder = WebApplication.CreateBuilder(args);

string DbConnection = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<FinanceManagerContext>(options => options.UseSqlServer(DbConnection));
builder.Services.AddScoped<IOperationService, OperationService>();
builder.Services.AddScoped<ITransactionService, TransactionService>();
builder.Services.AddScoped<IReportService, ReportService>();
builder.Services.AddControllers();

var app = builder.Build();

app.UseRouting();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.MapGet("/", () => "Welcome page");

app.Run();