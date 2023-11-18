#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Microservices/USOD.ProjectAPI/USOD.ProjectAPI.csproj", "Microservices/USOD.ProjectAPI/"]
COPY ["Domain/Models/Project_Library/Project_Library.csproj", "Domain/Models/Project_Library/"]
RUN dotnet restore "Microservices/USOD.ProjectAPI/USOD.ProjectAPI.csproj"
COPY . .
WORKDIR "/src/Microservices/USOD.ProjectAPI"
RUN dotnet build "USOD.ProjectAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "USOD.ProjectAPI.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "USOD.ProjectAPI.dll"]