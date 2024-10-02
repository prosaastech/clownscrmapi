# Use the official Microsoft .NET 8.0 ASP.NET runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

# Use the official Microsoft .NET 8.0 SDK image for building the app
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["ClownsCRMAPI.csproj", "./"]
RUN dotnet restore "./ClownsCRMAPI.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "ClownsCRMAPI.csproj" -c Release -o /app/build

# Publish the application
FROM build AS publish
RUN dotnet publish "ClownsCRMAPI.csproj" -c Release -o /app/publish

# Use the base image for the final stage
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ClownsCRMAPI.dll"]
