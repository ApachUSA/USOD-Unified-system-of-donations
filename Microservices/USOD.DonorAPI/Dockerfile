#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Microservices/USOD.DonorAPI/USOD.DonorAPI.csproj", "Microservices/USOD.DonorAPI/"]
COPY ["Domain/Models/Donor.Library/Donor_Library.csproj", "Domain/Models/Donor.Library/"]
RUN dotnet restore "Microservices/USOD.DonorAPI/USOD.DonorAPI.csproj"
COPY . .
WORKDIR "/src/Microservices/USOD.DonorAPI"
RUN dotnet build "USOD.DonorAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "USOD.DonorAPI.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "USOD.DonorAPI.dll"]