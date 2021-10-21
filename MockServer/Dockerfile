FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["MockServer/MockServer.csproj", "MockServer/"]
RUN dotnet restore "MockServer/MockServer.csproj"
COPY . .
WORKDIR "/src/MockServer"
RUN dotnet build "MockServer.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MockServer.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MockServer.dll"]
