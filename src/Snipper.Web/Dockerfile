FROM mcr.microsoft.com/dotnet/core/sdk:3.1 as build-env
WORKDIR /app

RUN apt update
RUN apt install -y nodejs npm

# Copy csproj and restore as distinct layers
COPY *.csproj ./
RUN dotnet restore

# Copy everything else and build
COPY . ./

# RUN npm cache clean --force
WORKDIR /app/ClientApp

RUN npm install

RUN npm rebuild node-sass

WORKDIR /app

RUN dotnet publish -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
WORKDIR /app
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "Snipper.Web.dll"]
