using Innowise.Common.Exceptions;
using Innowise.Common.Services;
using Innowise.Offices.Application;
using Innowise.Offices.Infrastructure;
using Innowise.Offices.Infrastructure.Persistence;
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
       .AddPolly()
       .AddInfrastructure(builder.Configuration)
       .AddAuth(builder.Configuration.GetSection("JwtOptions").Bind);

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

await DatabaseMigrationChecker.EnsureDatabaseIsFullyMigrated(app.Services);

app.Run();
