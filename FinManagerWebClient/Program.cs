using FinManagerWebClient.Library.Requests;
using FinManagerWebClient.RequestServices;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddHttpClient();
builder.Services.AddScoped<ITransactionService, TransactionService>();
builder.Services.AddScoped<IOperationService, OperationService>();
builder.Services.AddScoped<IReportsService, ReportsService>();

var app = builder.Build();

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
