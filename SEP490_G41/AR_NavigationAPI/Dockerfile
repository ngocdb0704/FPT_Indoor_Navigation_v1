#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["AR_NavigationAPI/AR_NavigationAPI.csproj", "AR_NavigationAPI/"]
COPY ["BusinessObject/BusinessObject.csproj", "BusinessObject/"]
COPY ["DataAccess/DataAccess.csproj", "DataAccess/"]
RUN dotnet restore "AR_NavigationAPI/AR_NavigationAPI.csproj"
COPY . .
WORKDIR "/src/AR_NavigationAPI"
RUN dotnet build "AR_NavigationAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AR_NavigationAPI.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AR_NavigationAPI.dll"]