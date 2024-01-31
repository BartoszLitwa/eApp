`kubectl create secret generic mssql --from-literal=SA_PASSWORD="admin123!"`

run in `/src/` directory

`docker build -t bartoszlitwa/eapp.commandservice.api -f CommandService/Dockerfile .`

`docker build -t bartoszlitwa/eapp.platformservice.api -f PlatformService\Dockerfile .`