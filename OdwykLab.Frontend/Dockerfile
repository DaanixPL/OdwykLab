# Build and publish OdwykLab.Frontend (Blazor Server/Web)
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# copy solution and project files for layer caching
COPY ["OdwykLab.sln", "./"]
COPY ["OdwykLab.Frontend/OdwykLab.Frontend.csproj", "OdwykLab.Frontend/"]
COPY ["OdwykLab.Domain/OdwykLab.Domain.csproj", "OdwykLab.Domain/"]

RUN dotnet restore "OdwykLab.Frontend/OdwykLab.Frontend.csproj"

# copy remaining files and publish
COPY . .
WORKDIR /src/OdwykLab.Frontend
RUN dotnet publish -c Release -o /app/publish --no-restore

# runtime image (frontend targets net10.0)
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS runtime
WORKDIR /app
ENV ASPNETCORE_URLS=http://+:80
COPY --from=build /app/publish .
EXPOSE 80
ENTRYPOINT ["dotnet", "OdwykLab.Frontend.dll"]