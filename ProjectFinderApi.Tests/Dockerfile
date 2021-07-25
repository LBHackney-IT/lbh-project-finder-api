FROM mcr.microsoft.com/dotnet/core/sdk:3.1

# disable microsoft telematry
ENV DOTNET_CLI_TELEMETRY_OPTOUT='true'

ENV DynamoDb_LocalMode='true'

WORKDIR /app

# Copy csproj and restore as distinct layers
COPY ./ProjecFinderApi.sln ./
COPY ./ProjecFinderApi/ProjecFinderApi.csproj ./ProjecFinderApi/
COPY ./ProjecFinderApi.Tests/ProjecFinderApi.Tests.csproj ./ProjecFinderApi.Tests/

RUN dotnet restore ./ProjecFinderApi/ProjecFinderApi.csproj
RUN dotnet restore ./ProjecFinderApi.Tests/ProjecFinderApi.Tests.csproj

# Copy everything else and build
COPY . .

RUN dotnet build -c debug -o out ProjecFinderApi.Tests/ProjecFinderApi.Tests.csproj

CMD dotnet test
