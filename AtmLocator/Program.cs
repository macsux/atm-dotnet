using AtmLocator.Clients;
using AtmLocator.Services;
using AtmLocator.Services.Impl;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddTransient<IATMService, DefaultAtmService>();
builder.Services.AddHttpClient<ILocTranslationService, DefaultLocTranslationService>(c =>
{
    c.BaseAddress = new Uri(builder.Configuration.GetValue<string>("services:location-transation"));
});

builder.Services.AddHttpClient<IBranchClient, BranchClient>(c =>
{
    c.BaseAddress = new Uri(builder.Configuration.GetValue<string>("services:branch"));
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
