# Stage 1: Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy toàn bộ mã nguồn
COPY . .

# Restore dependencies
RUN dotnet restore "AgricultureSmart.API/AgricultureSmart.API.csproj"

# Build project
WORKDIR /src/AgricultureSmart.API
RUN dotnet build "AgricultureSmart.API.csproj" -c Release -o /app/build

# Stage 2: Publish
FROM build AS publish
RUN dotnet publish "AgricultureSmart.API.csproj" -c Release -o /app/publish

# Stage 3: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .
EXPOSE 80
ENTRYPOINT ["dotnet", "AgricultureSmart.API.dll"]
