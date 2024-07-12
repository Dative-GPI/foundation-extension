FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build-env

WORKDIR /app

COPY dev dev

RUN dotnet publish dev/helpers/Foundation.Extension.Proxy -c Release -o /app/out

# build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:7.0

WORKDIR /app
COPY --from=build-env /app/out .

EXPOSE 80

ENTRYPOINT ["dotnet", "Foundation.Extension.Proxy.dll"]
