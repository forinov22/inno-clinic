using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Profiles.Application.Receptionists.Commands.Create;
using Profiles.Domain.Entities;

namespace Profiles.Infrastructure.Persistence;

public static class DbInitializer
{
    public static async Task Initialize(IServiceProvider serviceProvider)
    {
        var scope = serviceProvider.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<ProfilesDbContext>();
        var sender = scope.ServiceProvider.GetRequiredService<ISender>();

        if (!context.Receptionists.Any())
        {
            await sender.Send(
                new CreateReceptionistProfileCommand("forinovegor@gmail.com", "Yahor", "Forynau", null,
                                                     WorkerStatus.AtWork.ToString()));
        }
    }
}