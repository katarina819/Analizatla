# === BUILD STAGE ===
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Kopiraj csproj i restore dependencija
COPY *.csproj ./
RUN dotnet restore

# Kopiraj ostatak koda i buildaj
COPY . ./
RUN dotnet publish -c Release -o /app

# === RUNTIME STAGE ===
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app

# Kopiraj buildane fajlove
COPY --from=build /app ./

# Port koji Render koristi (dinamički preko PORT env varijable)
ENV PORT=8080
ENV ASPNETCORE_URLS=http://+:${PORT}
EXPOSE 8080

# Start aplikacije
ENTRYPOINT ["dotnet", "BACKEND.dll"]
