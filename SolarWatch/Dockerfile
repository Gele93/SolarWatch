# Build Stage
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env
WORKDIR /app

COPY *.csproj ./
RUN dotnet restore

COPY . . 
RUN dotnet publish -c Release -o out

RUN apt-get update && apt-get install -y wait-for-it

# Run database migration in the build stage
RUN dotnet tool install --global dotnet-ef --version 7.0.20
ENV PATH="$PATH:/root/.dotnet/tools"

FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app

COPY --from=build-env /app/out .

RUN mkdir -p /app/city-raw-data
COPY city-raw-data/citynames.csv /app/city-raw-data/citynames.csv

EXPOSE 5000

ENTRYPOINT ["dotnet", "SolarWatch.dll"]
