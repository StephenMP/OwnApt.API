FROM microsoft/dotnet:1.0.0-preview2-sdk

RUN mkdir /app
WORKDIR /app

COPY . /app
RUN ["dotnet", "restore"]
RUN ["dotnet", "build"]

EXPOSE 5000/tcp
ENTRYPOINT ["dotnet", "run"]
