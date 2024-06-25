#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["AR_Navigation/AR_Navigation.csproj", "AR_Navigation/"]
RUN dotnet restore "AR_Navigation/AR_Navigation.csproj"
COPY . .
WORKDIR "/src/AR_Navigation"
RUN dotnet build "AR_Navigation.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AR_Navigation.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AR_Navigation.dll"]