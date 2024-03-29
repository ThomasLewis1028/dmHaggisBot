FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["SWNBlazorApp/SWNBlazorApp.csproj", "SWNBlazorApp/"]
COPY ["SWNUniverseGenerator/SWNUniverseGenerator.csproj", "SWNUniverseGenerator/"]
RUN dotnet restore "SWNBlazorApp/SWNBlazorApp.csproj"
COPY . .
WORKDIR "/src/SWNBlazorApp"
RUN dotnet build "SWNBlazorApp.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "SWNBlazorApp.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SWNBlazorApp.dll"]
