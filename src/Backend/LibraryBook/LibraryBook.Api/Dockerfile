#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["LibraryBook.Api/LibraryBook.Api.csproj", "LibraryBook.Api/"]
COPY ["LibraryBook.Business/LibraryBook.Business.csproj", "LibraryBook.Business/"]
COPY ["LibraryBook.EF/LibraryBook.EF.csproj", "LibraryBook.EF/"]
RUN dotnet restore "LibraryBook.Api/LibraryBook.Api.csproj"
COPY . .
WORKDIR "/src/LibraryBook.Api"
RUN dotnet build "LibraryBook.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "LibraryBook.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "LibraryBook.Api.dll"]