FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env

WORKDIR /app
COPY . .
RUN dotnet restore
WORKDIR "/app"
RUN dotnet publish LocalEmbedding/LocalEmbedding.csproj -c Release -o /app/build

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime

EXPOSE 8080

WORKDIR /app
COPY --from=build-env /app/build .
# ENTRYPOINT ["dotnet", "LocalEmbedding.dll"]