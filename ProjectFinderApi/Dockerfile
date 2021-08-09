FROM mcr.microsoft.com/dotnet/core/sdk:3.1

WORKDIR /app

# Copy csproj and restore as distinct layers
COPY *.csproj ./
RUN dotnet restore

# Copy everything else and build
COPY . ./
RUN dotnet build -c Release -o out
RUN dotnet publish -c Release -o out

EXPOSE ${PORT:-8080}
CMD ASPNETCORE_URLS=http://+:${PORT:-8080} dotnet ./out/ProjectFinderApi.dll
