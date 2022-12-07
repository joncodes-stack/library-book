FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env
WORKDIR /app

# Copy everything
COPY . ./
# Restore as distinct layers
RUN dotnet restore
# Build and publish a release
RUN dotnet publish LibraryBook.Api -c -o out /p:DebugType=None /p:DebugSymbols=false

# Build runtime image
FROM mcr.microsoft.com/dotnet/sdk:6.0
WORKDIR /app
COPY --from=build-env /app/out .
EXPOSE 5001
ENTRYPOINT ["dotnet", "LibraryBook.Api.dll"]