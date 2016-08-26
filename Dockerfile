FROM microsoft/aspnet:latest

COPY . /app
COPY ../DevOps/NugetPackages /app
WORKDIR /app
RUN ["dnu", "restore"]

EXPOSE 5000  
ENTRYPOINT ["dnx", "-p", "src/Api/project.json", "docker-web"]
