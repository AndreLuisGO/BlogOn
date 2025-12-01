FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["BlogOnAPI.csproj", "./"]
RUN dotnet restore "BlogOnAPI.csproj"
    
COPY . .
WORKDIR "/src/."
RUN dotnet build "BlogOnAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BlogOnAPI.csproj" -c Release -o /app/publish /p:UseAppHost=false
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

USER app

COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BlogOnAPI.dll"]