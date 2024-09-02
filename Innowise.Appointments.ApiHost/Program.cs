using Appointments.Application;
using Appointments.Infrastructure;
using Appointments.Infrastructure.Persistence;
using Innowise.Common.Exceptions;
using Innowise.Common.Services;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Host.UseSerilog((context, configuration)
                            => configuration.ReadFrom.Configuration(context.Configuration));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddProblemDetails();
builder.Services.AddExceptionHandler<ValidationExceptionHandlingMiddleware>();
builder.Services.AddExceptionHandler<ExceptionHandlingMiddleware>();

builder.Services
       .AddApplication()
       .AddPersistence(builder.Configuration)
       .AddHttpClients(builder.Configuration)
       .AddMassTransit()
       .AddPolly()
       .AddRedis(builder.Configuration)
       .AddPdf()
       .AddAuth(builder.Configuration.GetSection("JwtOptions").Bind)
       .AddEmail(builder.Configuration.GetSection("EmailOptions").Bind);

var app = builder.Build();

app.UseSerilogRequestLogging();

app.UseExceptionHandler();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

if (app.Environment.EnvironmentName != "Test")
{
    await DatabaseMigrationChecker.EnsureDatabaseIsFullyMigrated(app.Services);
}

app.Run();

public partial class Program { }