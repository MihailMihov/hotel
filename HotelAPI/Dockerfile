FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["HotelAPI/HotelAPI.csproj", "HotelAPI/"]
RUN dotnet restore "HotelAPI/HotelAPI.csproj"
COPY . .
WORKDIR "/src/HotelAPI"
RUN dotnet build "HotelAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "HotelAPI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "HotelAPI.dll"]
