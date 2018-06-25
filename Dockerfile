FROM microsoft/aspnetcore-build AS build-env
WORKDIR /app

# Copy csproj and restore as distinct layers
COPY ./PokemonApi/PokemonApi.csproj ./PokemonApi/
RUN dotnet restore ./PokemonApi/

# Copy everything else and build
COPY . ./

RUN dotnet publish ./PokemonApi/ -o /publish --configuration Release

# Build runtime image
FROM microsoft/aspnetcore
WORKDIR /app
COPY --from=build-env /publish .
ENTRYPOINT ["dotnet", "PokemonApi.dll"]