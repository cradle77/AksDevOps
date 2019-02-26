FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY Backend.Specs/ Backend.Specs/

WORKDIR /src/Backend.Specs
RUN dotnet restore
ENTRYPOINT ["dotnet", "test"]
