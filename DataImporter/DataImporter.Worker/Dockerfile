FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
RUN apt-get update && apt-get install -y nodejs
COPY ["DataImporter.Worker/*.csproj", "DataImporter.Worker/"]
COPY ["DataImporter.Data/*.csproj", "DataImporter.Data/"]
COPY ["DataImporter.Common/*.csproj", "DataImporter.Common/"]
COPY ["DataImporter.FileRead/*.csproj", "DataImporter.FileRead/"]
COPY ["DataImporter.SystemImporter/*.csproj", "DataImporter.SystemImporter/"]
RUN dotnet restore "DataImporter.Worker/DataImporter.Worker.csproj"
COPY . .
WORKDIR "/src/DataImporter.Worker"
RUN dotnet build "DataImporter.Worker.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "DataImporter.Worker.csproj" -c Release -o /app

FROM build AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "DataImporter.Worker.dll"]