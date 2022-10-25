using TransactionalOutBoxPattern.Application;
using TransactionalOutBoxPattern.Infrastructure;
using TransactionalOutBoxPattern.Infrastructure.Persistence;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;

services.AddEndpointsApiExplorer();
services.AddSwaggerGen();
services.AddControllers();
services.AddApplicationServices();
services.AddInfrastructureServices();

var app = builder.Build();

await app
    .Services
    .CreateScope()
    .ServiceProvider
    .GetRequiredService<IDatabaseMigration>()
    .MigrateAsync();

app.UseSwagger();
app.UseSwaggerUI();

app.MapControllers();

app.Run();
