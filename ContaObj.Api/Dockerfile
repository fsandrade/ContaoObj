#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["ContaObj.Api/ContaObj.Api.csproj", "ContaObj/"]
RUN dotnet restore "ContaObj.Api/ContaObj.Api.csproj"
COPY . .
WORKDIR "/src/ContaObj.Api"
RUN dotnet build "ContaObj.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ContaObj.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ContaObj.Api.dll"]