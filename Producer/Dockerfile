# Use the official .NET SDK image as the base
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

# Set the working directory inside the container
WORKDIR /app

# Copy the project files to the working directory
COPY . .

# Restore the project dependencies
RUN dotnet restore

# Build the application
RUN dotnet build -c Release -o out

# Use the runtime image for running the app
FROM mcr.microsoft.com/dotnet/runtime:8.0

# Set the working directory inside the container
WORKDIR /app

# Copy the built application from the build stage
COPY --from=build /app/out .

# Entry point for the console app
ENTRYPOINT ["dotnet", "Producer.dll"]
