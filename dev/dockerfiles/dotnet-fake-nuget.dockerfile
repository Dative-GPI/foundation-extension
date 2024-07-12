# Let's build nuget packages

FROM alpine as nuget-env

WORKDIR /app

COPY src/app src/app
COPY src/context src/context
COPY src/shared src/shared

# On supprime tous les fichiers excepté les csproj
RUN find . ! -name '*.csproj' -type f -exec rm -f {} +

# On supprime les dossiers qui servent plus à rien
RUN find . -type d -empty -delete

# ----------------------------------------

FROM mcr.microsoft.com/dotnet/sdk:7.0 as nuget-build

ARG TEMPLATE_VERSION
ENV TEMPLATE_VERSION=$TEMPLATE_VERSION

WORKDIR /app

COPY --from=nuget-env /app /app

RUN find . -name '*.csproj' -type f -exec dotnet restore {} \;

RUN mkdir /out

COPY src/app src/app
COPY src/context src/context
COPY src/shared src/shared

RUN find . -name '*.csproj' -type f -exec dotnet pack {} --configuration Release -p:PackageVersion=${TEMPLATE_VERSION} -o /out/ \;


# ----------------------------------------
# Let's build the app with the nuget packages

FROM alpine as proj-env

WORKDIR /app

COPY src/template src/template

# On supprime tous les fichiers excepté les csproj
RUN find . ! -name '*.csproj' -type f -exec rm -f {} +

# On supprime les dossiers qui servent plus à rien
RUN find . -type d -empty -delete

# ----------------------------------------

FROM mcr.microsoft.com/dotnet/sdk:7.0

# install debugger for NET Core
RUN curl -sSL https://aka.ms/getvsdbgsh | /bin/sh /dev/stdin -v latest -l ~/vsdbg

ARG PROJECT
ARG POST_RESTORE
ARG PRE_BUILD
ARG PRE_BUILD2
ARG TEMPLATE_VERSION
ENV TEMPLATE_VERSION=$TEMPLATE_VERSION

WORKDIR /app

COPY --from=nuget-build /out /nuget

RUN ls /nuget

RUN dotnet nuget add source /nuget

WORKDIR /app/$PROJECT
COPY --from=proj-env /app /app

RUN dotnet restore 
RUN $POST_RESTORE

COPY . /app

RUN $PRE_BUILD
RUN $PRE_BUILD2

RUN dotnet build --no-restore

ENTRYPOINT dotnet run --no-build --no-restore
