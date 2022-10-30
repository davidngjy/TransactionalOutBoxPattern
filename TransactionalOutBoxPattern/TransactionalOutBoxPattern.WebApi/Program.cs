using TransactionalOutBoxPattern.Application;
using TransactionalOutBoxPattern.Infrastructure;
using TransactionalOutBoxPattern.WebApi.BackgroundServices;
using TransactionalOutBoxPattern.WebApi.Extensions;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;

services.AddEndpointsApiExplorer();
services.AddHostedService<OutboxMessageBackgroundService>();
services.AddSwaggerGen();
services.AddControllers();
services.AddApplicationServices();
services.AddInfrastructureServices();

var app = builder.Build();

app.MigrateDatabase();

app.UseSwagger();
app.UseSwaggerUI();

app.MapControllers();

app.Run();
