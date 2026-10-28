FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

COPY src/ src/
COPY tests/ tests/
COPY feature-flow.sln ./

RUN dotnet restore tests/FeatureFlow.Example.Api.IntegrationTests/FeatureFlow.Example.Api.IntegrationTests.csproj
RUN dotnet build tests/FeatureFlow.Example.Api.IntegrationTests/FeatureFlow.Example.Api.IntegrationTests.csproj -c Release --no-restore

FROM build AS test
WORKDIR /src
ENTRYPOINT [ "dotnet", "test", "tests/FeatureFlow.Example.Api.IntegrationTests/FeatureFlow.Example.Api.IntegrationTests.csproj", "-c", "Release", "--no-build", "--logger", "trx" ]
