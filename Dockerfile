FROM mcr.microsoft.com/dotnet/aspnet:5.0-alpine AS base
WORKDIR /app
EXPOSE 80 443

FROM mcr.microsoft.com/dotnet/sdk:5.0-alpine AS build
WORKDIR /src
COPY . .
RUN dotnet restore "LabApi/LabApi.csproj"

WORKDIR "/src/LabApi"
RUN dotnet build "LabApi.csproj" -c Release -o /app/build
RUN dotnet publish "LabApi.csproj" -c Release -o /app/publish

FROM base AS final
RUN apk add --no-cache icu-libs
ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "LabApi.dll"]