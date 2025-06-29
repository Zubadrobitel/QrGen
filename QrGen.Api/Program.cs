using Application.Services;
using Microsoft.EntityFrameworkCore;
using QrGen.DataBase;
using QrGen.DataBase.Repositories;
using QrGen.Domain.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ApplicationDataBaseContext>(
    options =>
    {
        options.UseNpgsql(builder.Configuration.GetConnectionString(nameof(ApplicationDataBaseContext)));
    });

builder.Services.AddScoped<IQrCodeService, QrCodeService>();
builder.Services.AddScoped<IQrRepository, QrRepository>();
builder.Services.AddScoped<IQrCodeGenerator, QrCodeGenerator.Service.QrCodeGenerator>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
