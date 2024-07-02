using Appointments.Application;
using Appointments.Infrastructure;
using Appointments.Infrastructure.Persistence;
using InnoClinic.Exceptions;
using InnoClinic.Services;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Host.UseSerilog((context, configuration) => configuration.ReadFrom.Configuration(context.Configuration));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddProblemDetails();
builder.Services.AddHttpClient();
builder.Services.AddExceptionHandler<ValidationExceptionHandlingMiddleware>();
builder.Services.AddExceptionHandler<ExceptionHandlingMiddleware>();

builder.Services
       .AddApplication()
       .AddInfrastructure(builder.Configuration)
       .AddAuth(builder.Configuration)
       .AddEmail(builder.Configuration);

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

if (app.Environment.IsDevelopment())
{
    DatabaseMigrator.MigrateDatabase(app.Services);
}

app.Run();