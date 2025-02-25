# Dockerfile
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env
WORKDIR /app

# Copy csproj and restore as distinct layers
COPY *.csproj ./
RUN dotnet restore


# Copy everything else and build
COPY . ./
RUN dotnet publish -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
COPY --from=build-env /app/out .

# Create the directory in the container
RUN mkdir -p /app/city-raw-data

# Copy the CSV file into the directory
COPY city-raw-data/citynames.csv /app/city-raw-data/citynames.csv

# Copy the .env file to the container
COPY .env .env

# Set environment variables for docker container
ENV ASPNETCORE_ENVIRONMENT=Production


ENTRYPOINT ["dotnet", "SolarWatch.dll"]