using FinanceManager.Library.Interfaces;
using FinanceManager.Services;
using Microsoft.EntityFrameworkCore;
using FinanceManager.EndpointHandlers;
using FinanceManager.DAL;

var builder = WebApplication.CreateBuilder(args);

string DbConnection = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<FinanceManagerContext>(options => options.UseSqlServer(DbConnection));
builder.Services.AddTransient<IOperationService, OperationService>();
builder.Services.AddTransient<ITransactionService, TransactionService>();
builder.Services.AddTransient<IReportService, ReportService>();
builder.Services.AddControllers();

var app = builder.Build();

app.UseRouting();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.MapGet("/", () => "Welcome page");

app.Run();