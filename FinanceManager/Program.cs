using FinanceManager.Library.Interfaces;
using FinanceManager.Model;
using FinanceManager.Services;
using Microsoft.EntityFrameworkCore;
using FinanceManager.EndpointHandlers;

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

//app.MapPost("/operations", OperationEndpointsHandler.AddAsync);
//app.MapPut("/operations", OperationEndpointsHandler.EditAsync);
//app.MapGet("/operations", OperationEndpointsHandler.GetAllAsync);
//app.MapGet("/operations/{id:int}", OperationEndpointsHandler.GetOperationAsync);
//app.MapDelete("/operations/{id:int}", OperationEndpointsHandler.RemoveAsync);

app.MapPost("/transactions", TransactionEndpointsHandler.AddAsync);
app.MapPut("/transactions", TransactionEndpointsHandler.EditAsync);
app.MapGet("/transactions", TransactionEndpointsHandler.GetAllAsync);
app.MapGet("/transactions/{id:int}", TransactionEndpointsHandler.GetTransactionAsync);
app.MapDelete("/transactions/{id:int}", TransactionEndpointsHandler.RemoveAsync);

app.MapGet("/dailyReport/{date}", ReportEndpointsHandler.GetDailyReportAsync);
app.MapGet("/periodReport/{startDate}-{endDate}", ReportEndpointsHandler.GetPeriodReportAsync);

app.Run();