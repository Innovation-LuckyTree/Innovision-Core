# Use the Microsoft .NET Core SDK image to build the solution
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build-env
WORKDIR /app

# Copy csproj and restore as distinct layers
COPY *.sln .
COPY Innovision.Core.API/*.csproj ./Innovision.Core.API/
COPY Innovision.Core.Application/*.csproj ./Innovision.Core.Application/
COPY Innovision.Core.Common/*.csproj ./Innovision.Core.Common/
COPY Innovision.Core.Domain/*.csproj ./Innovision.Core.Domain/
COPY Innovision.Core.Infrastructure/*.csproj ./Innovision.Core.Infrastructure/
COPY Innovision.Core.Persistence/*.csproj ./Innovision.Core.Persistence/
COPY Innovision.Core.API/Assets/*.json ./Assets/
RUN dotnet restore

# Copy everything else and build
COPY . ./
RUN dotnet publish Innovision.Core.API -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /app
COPY --from=build-env /app/out .

Expose 8080

ENTRYPOINT ["dotnet", "Innovision.Core.API.dll"]

