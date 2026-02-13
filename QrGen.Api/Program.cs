using Application.Services;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using QrGen.DataBase;
using QrGen.DataBase.Repositories;
using QrGen.Domain.Interfaces;
using QrGen.Domain.Model;
using Transit.Consumers;
using Transit.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ApplicationDataBaseContext>(
    options =>
    {
        options.UseNpgsql(builder.Configuration.GetConnectionString(nameof(ApplicationDataBaseContext)));
    });

builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<ConsumerBase<QrCode>>();
    x.AddConsumer<ConsumerBase<QrInfo>>();

    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host("rabbitmq", "/", h =>
        {
            h.Username("guest");
            h.Password("guest");
        });

        cfg.ReceiveEndpoint("qr-generated-queue", e =>
        {
            e.ConfigureConsumer<ConsumerBase<QrCode>>(context);
            e.ConfigureConsumer<ConsumerBase<QrInfo>>(context);
        });
    });
});

try
{
    builder.Services.AddScoped<IQrCodeService, QrCodeService>();
    builder.Services.AddScoped<IQrRepository, QrRepository>();
    builder.Services.AddScoped<IQrCodeGenerator, QrCodeGenerator.Service.QrCodeGenerator>();
    builder.Services.AddScoped<IEventPublisher, MassTransitEventPublisher>();
}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
}


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDataBaseContext>();
    try
    {
        if (await dbContext.Database.CanConnectAsync())
        {
            Console.WriteLine("Соединение с базой данных установлено");
            var migrations = await dbContext.Database.GetPendingMigrationsAsync();

            if (migrations.Any())
            {
                Console.WriteLine("Найдены неприменённые миграции");
                await dbContext.Database.MigrateAsync();
                Console.WriteLine("Миграции приминены");
            }
            else
            {
                Console.WriteLine("База данных в актуальном состоянии");
            }
        }
        else
        {
            Console.WriteLine("Ошибка подключения к базе данных, попытка инициализации базы данных");
            await dbContext.Database.MigrateAsync();

            if (await dbContext.Database.CanConnectAsync())
                Console.WriteLine("Инициализация прошла успешно");
            else
                Console.WriteLine("Ошибка подключения к базе данных");
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine("Ошибка при инициализации/подключении к БД: " + ex.Message);
    }
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
