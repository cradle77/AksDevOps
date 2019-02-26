FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY Frontend.UiTests/ Frontend.UiTests/

WORKDIR /src/Frontend.UiTests
RUN dotnet restore
ENTRYPOINT ["dotnet", "test"]
