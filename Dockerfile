FROM microsoft/aspnet:latest

EXPOSE 5000  
ENTRYPOINT ["dnx", "-p", "src/Api/project.json", "docker-web"]

COPY src/Api/project.json /app/  
WORKDIR /app  
RUN ["dnu", "restore"]  
COPY . /app 
