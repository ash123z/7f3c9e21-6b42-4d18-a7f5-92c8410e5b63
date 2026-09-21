# Multi-stage build
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# Copy solution and project files
COPY ["LongestIncreasingSubsequence.sln", "."]
COPY ["src/LIS.Core/LIS.Core.csproj", "src/LIS.Core/"]
COPY ["src/LIS.App/LIS.App.csproj", "src/LIS.App/"]
COPY ["tests/LIS.Tests/LIS.Tests.csproj", "tests/LIS.Tests/"]

# Restore dependencies
RUN dotnet restore "LongestIncreasingSubsequence.sln"

# Copy source code
COPY . .

# Build
RUN dotnet build "LongestIncreasingSubsequence.sln" -c Release --no-restore

# Publish
RUN dotnet publish "src/LIS.App/LIS.App.csproj" -c Release --no-build -o /app/publish

# Runtime stage
FROM mcr.microsoft.com/dotnet/runtime:10.0
WORKDIR /app
COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "LIS.App.dll"]
