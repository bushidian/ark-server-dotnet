FROM microsoft/aspnet

MAINTAINER Bushidian

COPY . /app
WORKDIR /app

RUN ['dotnet', 'restore']

EXPOSE 5004

ENTRYPOINT ["dotnet", "run"]
