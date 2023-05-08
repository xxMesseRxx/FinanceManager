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

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.MapGet("/operations", OperationEndpointsHandler.GetAllAsync);
app.MapGet("/operations/{id:int}", OperationEndpointsHandler.GetOperationAsync);

app.MapGet("/transactions", TransactionEndpointsHandler.GetAllAsync);

app.Run();