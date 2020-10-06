FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /app

# copy csproj and restore as distinct layers
COPY *.sln .
COPY UMS/*.csproj ./UMS/
COPY EmailService/*.csproj ./EmailService/
RUN dotnet restore

# copy everything else and build app
COPY UMS/. ./UMS/
COPY EmailService/. ./EmailService/
WORKDIR /app/UMS
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS runtime
WORKDIR /app
COPY --from=build /app/UMS/out ./
ENTRYPOINT ["dotnet", "UMS.dll"]