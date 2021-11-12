FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
RUN apt-get update && apt-get install -y nodejs
COPY ["DataImporter.Web/*.csproj", "DataImporter.Web/"]
COPY ["DataImporter.Data/*.csproj", "DataImporter.Data/"]
COPY ["DataImporter.Common/*.csproj", "DataImporter.Common/"]
COPY ["DataImporter.FileRead/*.csproj", "DataImporter.FileRead/"]
COPY ["DataImporter.Membership/*.csproj", "DataImporter.Membership/"]
COPY ["DataImporter.SystemImporter/*.csproj", "DataImporter.SystemImporter/"]
RUN dotnet restore "DataImporter.Web/DataImporter.Web.csproj"
COPY . .
WORKDIR "/src/DataImporter.Web"
RUN dotnet build "DataImporter.Web.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "DataImporter.Web.csproj" -c Release -o /app

FROM build AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "DataImporter.Web.dll"]