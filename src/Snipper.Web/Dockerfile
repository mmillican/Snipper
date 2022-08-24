FROM mcr.microsoft.com/dotnet/aspnet:6.0.1 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0.100 AS build

WORKDIR /tmp
COPY ["src/Snipper.Web/Snipper.Web.csproj", "src/Snipper.Web/"]

RUN dotnet restore "src/Snipper.Web/Snipper.Web.csproj"

COPY . .

RUN apt update
RUN apt install -y nodejs npm

# WORKDIR "/tmp/src/Snipper.Web/ClientApp"

# RUN npm install

# RUN npm rebuild node-sass

WORKDIR "/tmp/src/Snipper.Web"
RUN dotnet build "Snipper.Web.csproj" -c Release

FROM build AS publish
RUN dotnet publish "Snipper.Web.csproj" --no-build -c Release -o /app/publish

FROM base AS final

WORKDIR /app
COPY --from=publish /app/publish .

RUN sed -i 's/TLSv1.2/TLSv1.0/g' /etc/ssl/openssl.cnf

ENTRYPOINT [ "dotnet", "Snipper.Web.dll" ]

# # Copy csproj and restore as distinct layers
# COPY *.csproj ./
# RUN dotnet restore

# # Copy everything else and build
# COPY . ./

# # RUN npm cache clean --force
# WORKDIR /app/ClientApp

# RUN npm install

# RUN npm rebuild node-sass

# WORKDIR /app

# RUN dotnet publish -c Release -o out

# # Build runtime image
# FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
# WORKDIR /app
# COPY --from=build-env /app/out .
# ENTRYPOINT ["dotnet", "Snipper.Web.dll"]
