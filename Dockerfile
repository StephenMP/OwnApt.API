FROM microsoft/aspnet:latest

EXPOSE 5000  
ENTRYPOINT ["dnx", "-p", "src/Api/project.json", "docker-web"]

COPY src/Api/project.json /app/  

RUN mkdir -p ~/.config/NuGet/
COPY NuGet.config ~/.config/NuGet/

WORKDIR /app
RUN ["dnu", "restore"]  
COPY . /app 
