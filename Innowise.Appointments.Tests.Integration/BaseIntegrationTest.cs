using Appointments.Application.Interfaces;
using Appointments.Application.Interfaces.HttpClients.Services;
using Appointments.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.DependencyInjection;
using Polly.Registry;

namespace Innowise.Appointments.Tests.Integration;

public abstract class BaseIntegrationTest : IClassFixture<IntegrationTestWebAppFactory>
{
    private readonly IServiceScope _scope;
    protected readonly ISender _sender;
    protected readonly AppointmentsDbContext _context;
    protected readonly IUnitOfWork _unitOfWork;
    protected readonly IServiceHttpClient _serviceHttpClient;
    protected readonly IDistributedCache _distributedCache;
    protected readonly ResiliencePipelineProvider<string> _pipelineProvider;

    protected BaseIntegrationTest(IntegrationTestWebAppFactory factory)
    {
        _scope = factory.Services.CreateScope();
        _sender = _scope.ServiceProvider.GetRequiredService<ISender>();
        _context = _scope.ServiceProvider.GetRequiredService<AppointmentsDbContext>();

        _context.Database.Migrate();

        _unitOfWork = _scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
        _serviceHttpClient = _scope.ServiceProvider.GetRequiredService<IServiceHttpClient>();
        _distributedCache = _scope.ServiceProvider.GetRequiredService<IDistributedCache>();
        _pipelineProvider = _scope.ServiceProvider.GetRequiredService<ResiliencePipelineProvider<string>>();
    }
}