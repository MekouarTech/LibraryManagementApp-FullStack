FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY ["src/LibraryManagementApp.API/LibraryManagementApp.API.csproj", "src/LibraryManagementApp.API/"]
COPY ["src/LibraryManagementApp.Application/LibraryManagementApp.Application.csproj", "src/LibraryManagementApp.Application/"]
COPY ["src/LibraryManagementApp.Infrastructure/LibraryManagementApp.Infrastructure.csproj", "src/LibraryManagementApp.Infrastructure/"]
COPY ["src/LibraryManagementApp.Domain/LibraryManagementApp.Domain.csproj", "src/LibraryManagementApp.Domain/"]
RUN dotnet restore "src/LibraryManagementApp.API/LibraryManagementApp.API.csproj"
COPY . .
WORKDIR "/src/src/LibraryManagementApp.API"
RUN dotnet build "LibraryManagementApp.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "LibraryManagementApp.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "LibraryManagementApp.API.dll"] 