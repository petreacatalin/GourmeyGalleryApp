# Use the official .NET 8 SDK image as the build environment
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

# Set the working directory
WORKDIR /app

# Copy the project file and restore dependencies
COPY *.csproj ./
RUN dotnet restore "./GourmeyGalleryApp.csproj"

# Copy the rest of the application code
COPY . ./

# Build the application
RUN dotnet build -c Release -o /app/build

# Publish the application
RUN dotnet publish -c Release -o /app/publish

# Use the official .NET 8 runtime image for the runtime environment
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime

# Set the working directory
WORKDIR /app

# Copy the published application from the build stage
COPY --from=build /app/publish .

# Set the entry point for the application
ENTRYPOINT ["dotnet", "GourmeyGalleryApp.dll"]

# Expose the port the app runs on
EXPOSE 80
