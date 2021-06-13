#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["Forum.WebApi/Forum.WebApi.csproj", "Forum.WebApi/"]
COPY ["Forum.Infrastructure/Forum.Infrastructure.csproj", "Forum.Infrastructure/"]
COPY ["Forum.ApplicationLayer/Forum.ApplicationLayer.csproj", "Forum.ApplicationLayer/"]
COPY ["Forum.Domain/Forum.Domain.csproj", "Forum.Domain/"]
RUN dotnet restore "Forum.WebApi/Forum.WebApi.csproj"
COPY . .
WORKDIR "/src/Forum.WebApi"
RUN dotnet build "Forum.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Forum.WebApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Forum.WebApi.dll"]
