FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["HotelReservationApi/HotelReservationApi.csproj", "HotelReservationApi/"]
RUN dotnet restore "HotelReservationApi/HotelReservationApi.csproj"
COPY . .
WORKDIR "/src/HotelReservationApi"
RUN dotnet build "HotelReservationApi.csproj" -c Release -o /app
WORKDIR /src
COPY ["ServicesTests/ServicesTests.csproj", "ServicesTests/"]
RUN dotnet restore "ServicesTests/ServicesTests.csproj"
COPY . .
WORKDIR "/src/ServicesTests"
RUN dotnet build "ServicesTests.csproj" -c Release -o /app
RUN dotnet test

FROM build AS publish
WORKDIR "/src/HotelReservationApi"
RUN dotnet publish "HotelReservationApi.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "HotelReservationApi.dll"]