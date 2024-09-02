FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Innowise.Appointments.ApiHost/Innowise.Appointments.ApiHost.csproj", "Innowise.Appointments.ApiHost/"]
COPY ["Innowise.Appointments.Application/Innowise.Appointments.Application.csproj", "Innowise.Appointments.Application/"]
COPY ["Innowise.Appointments.Domain/Innowise.Appointments.Domain.csproj", "Innowise.Appointments.Domain/"]
COPY ["Innowise.Common/Innowise.Common.csproj", "Innowise.Common/"]
COPY ["Innowise.Appointments.Contracts/Innowise.Appointments.Contracts.csproj", "Innowise.Appointments.Contracts/"]
COPY ["Innowise.Appointments.Infrastructure/Innowise.Appointments.Infrastructure.csproj", "Innowise.Appointments.Infrastructure/"]
RUN dotnet restore "Innowise.Appointments.ApiHost/Innowise.Appointments.ApiHost.csproj"
COPY . .
WORKDIR "/src/Innowise.Appointments.ApiHost"
RUN dotnet build "Innowise.Appointments.ApiHost.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Innowise.Appointments.ApiHost.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Innowise.Appointments.ApiHost.dll"]
