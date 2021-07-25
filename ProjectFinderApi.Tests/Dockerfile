FROM mcr.microsoft.com/dotnet/core/sdk:3.1

# disable microsoft telematry
ENV DOTNET_CLI_TELEMETRY_OPTOUT='true'

ENV DynamoDb_LocalMode='true'

WORKDIR /app

# Copy csproj and restore as distinct layers
COPY ./ProjectFinderApi.sln ./
COPY ./ProjectFinderApi/ProjectFinderApi.csproj ./ProjectFinderApi/
COPY ./ProjectFinderApi.Tests/ProjectFinderApi.Tests.csproj ./ProjectFinderApi.Tests/

RUN dotnet restore ./ProjectFinderApi/ProjectFinderApi.csproj
RUN dotnet restore ./ProjectFinderApi.Tests/ProjectFinderApi.Tests.csproj

# Copy everything else and build
COPY . .

RUN dotnet build -c debug -o out ProjectFinderApi.Tests/ProjectFinderApi.Tests.csproj

CMD dotnet test
