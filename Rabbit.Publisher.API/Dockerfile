#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Rabbit.Publisher.API/Rabbit.Publisher.API.csproj", "Rabbit.Publisher.API/"]
RUN dotnet restore "Rabbit.Publisher.API/Rabbit.Publisher.API.csproj"
COPY . .
WORKDIR "/src/Rabbit.Publisher.API"
RUN dotnet build "Rabbit.Publisher.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Rabbit.Publisher.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Rabbit.Publisher.API.dll"]