using MassTransit;
using Microsoft.EntityFrameworkCore;
using Order.API.Models;
using Shared;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddDbContext<AppDbContext>(options => options.UseNpgsql(
    builder.Configuration.GetConnectionString("SqlConnection"), npgOptions =>
        npgOptions.MigrationsAssembly("Order.API")
));

//MassTransit
builder.Services.AddMassTransit(x =>
{
    x.UsingRabbitMq((context, conf) =>
{
    conf.Host(builder.Configuration.GetConnectionString("RabbitMq"));
    
});
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    await context.Database.MigrateAsync();
}

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