`kubectl create secret generic mssql --from-literal=SA_PASSWORD="admin123!"`

run in `/src/` directory

# Platform Service
`docker build -t bartoszlitwa/eapp.platformservice.api -f PlatformService\Dockerfile .`
`docker push bartoszlitwa/eapp.platformservice.api`

# Command Service
`docker build -t bartoszlitwa/eapp.commandservice.api -f CommandService/Dockerfile .`
`docker push bartoszlitwa/eapp.commandservice.api`

