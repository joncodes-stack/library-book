#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 44379

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["LibraryBook.Api/LibraryBook.Api.csproj", "LibraryBook.Api/"]
COPY ["LibraryBook.Business/LibraryBook.Application.csproj", "LibraryBook.Business/"]
COPY ["LibraryBook.Domain/LibraryBook.Domain.csproj", "LibraryBook.Domain/"]
COPY ["LibraryBook.CrossCutting/LibraryBook.CrossCutting.csproj", "LibraryBook.CrossCutting/"]
COPY ["LibraryBook.EF/LibraryBook.EF.csproj", "LibraryBook.EF/"]
COPY ["LibraryBook.Ioc/LibraryBook.Ioc.csproj", "LibraryBook.Ioc/"]
RUN dotnet restore "./LibraryBook.Api/./LibraryBook.Api.csproj"
COPY . .
WORKDIR "/src/LibraryBook.Api"
RUN dotnet build "./LibraryBook.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./LibraryBook.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Install the agent
RUN apt-get update && apt-get install -y wget ca-certificates gnupg \
&& echo 'deb http://apt.newrelic.com/debian/ newrelic non-free' | tee /etc/apt/sources.list.d/newrelic.list \
&& wget https://download.newrelic.com/548C16BF.gpg \
&& apt-key add 548C16BF.gpg \
&& apt-get update \
&& apt-get install -y 'newrelic-dotnet-agent' \
&& rm -rf /var/lib/apt/lists/*

# Enable the agent
ENV CORECLR_ENABLE_PROFILING=1 \
CORECLR_PROFILER={36032161-FFC0-4B61-B559-F6C5D41BAE5A} \
CORECLR_NEWRELIC_HOME=/usr/local/newrelic-dotnet-agent \
CORECLR_PROFILER_PATH=/usr/local/newrelic-dotnet-agent/libNewRelicProfiler.so \
NEW_RELIC_LICENSE_KEY=026b0ffc2fd0e27a8f171f9c970e57f0FFFFNRAL \
NEW_RELIC_APP_NAME="LibraryBook.Api"

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "LibraryBook.Api.dll"]



