FROM microsoft/dotnet

MAINTAINER Bushidian

COPY . /app
WORKDIR /app

RUN dotnet restore 

RUN dotnet publish -c Release -o out

EXPOSE 5004

ENTRYPOINT ["dotnet", "out/ark-server-dotnet.dll"]
