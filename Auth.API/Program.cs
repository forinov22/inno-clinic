using Auth.Application;
using Auth.Infrastructure;
using Auth.Infrastructure.Persistence;
using InnoClinic.Exceptions;
using InnoClinic.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddProblemDetails();
builder.Services.AddExceptionHandler<ValidationExceptionHandlingMiddleware>();
builder.Services.AddExceptionHandler<ExceptionHandlingMiddleware>();

builder.Services
    .AddApplication()
    .AddInfrastructure(builder.Configuration)
    .AddAuth(builder.Configuration.GetSection("JwtOptions").Bind)
    .AddEmail(builder.Configuration.GetSection("EmailOptions").Bind);

var app = builder.Build();

app.MapControllers();

app.UseExceptionHandler();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

if (app.Environment.IsDevelopment())
{
    DatabaseMigrator.MigrateDatabase(app.Services);
}

app.Run();
