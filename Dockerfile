FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base

WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS base

COPY . .


# Install all dependencies
RUN dotnet restore "TopNews.WEB/TopNews.WEB.csproj"

# Install the dotnet-ef tools
RUN dotnet tool install -g dotnet-ef
ENV PATH $PATH:/root/.dotnet/tools

RUN dotnet-ef database update --startup-project "TopNews.WEB" --project "TopNews.Infrastructure/TopNews.Infrastructure.csproj"

RUN dotnet publish "TopNews.Web/TopNews.WEB.csproj" -c Release -o /app/build
WORKDIR /app/build
EXPOSE 80

ENTRYPOINT ["dotnet", "TopNews.WEB.dll", "--urls=http://0.0.0.0:80"]
