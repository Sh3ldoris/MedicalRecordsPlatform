# Build stage
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# Copy csproj and restore dependencies
COPY ["MedicalRecords.csproj", "./"]
RUN dotnet restore "MedicalRecords.csproj"

# Copy everything else and build
COPY . .
RUN dotnet build "MedicalRecords.csproj" -c Release -o /app/build

# Publish stage
FROM build AS publish
RUN dotnet publish "MedicalRecords.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app
EXPOSE 8080

COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MedicalRecords.dll"]