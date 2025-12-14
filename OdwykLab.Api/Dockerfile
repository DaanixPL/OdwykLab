# Build and publish App.Api (backend)
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# copy solution and project files to leverage Docker layer cache
COPY ["OdwykLab.sln", "./"]
COPY ["OdwykLab.Api/OdwykLab.Api.csproj", "OdwykLab.Api/"]
COPY ["OdwykLab.Application/OdwykLab.Application.csproj", "OdwykLab.Application/"]
COPY ["OdwykLab.Domain/OdwykLab.Domain.csproj", "OdwykLab.Domain/"]
COPY ["OdwykLab.Infrastructure/OdwykLab.Infrastructure.csproj", "OdwykLab.Infrastructure/"]

RUN dotnet restore "OdwykLab.Api/OdwykLab.Api.csproj"

# copy everything and publish
COPY . .
WORKDIR /src/App.Api
RUN dotnet publish -c Release -o /app/publish --no-restore

# runtime image (backend targets net8.0)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
ENV ASPNETCORE_URLS=http://+:80
COPY --from=build /app/publish .
EXPOSE 80
ENTRYPOINT ["dotnet", "OdwykLab.Api.dll"]