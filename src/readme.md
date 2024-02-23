`kubectl create secret generic mssql --from-literal=SA_PASSWORD="admin123!"`

run in `/src/` directory

# Platform Service
`docker build -t bartoszlitwa/eapp.platformservice.api -f PlatformService\Dockerfile .`

`docker push bartoszlitwa/eapp.platformservice.api`

# Command Service
`docker build -t bartoszlitwa/eapp.commandservice.api -f CommandService/Dockerfile .`

`docker push bartoszlitwa/eapp.commandservice.api`

# Kubectl
`kubectl get deployment`

`kubectl get pods`

`kubectl delete pod {pod-name}`

`kubectl apply -f {file}`

`kubectl delete deployment {deployment-name}`

`kubectl rollout restart deployment {deployment-name}`

`kubectl get storageclass`

`kubectl get pvc`

`kubectl create secret generic mssql --from-literal={secret-name}="{secret-value}"`

`kubectl get secret`

`kubectl delete secret {secret-name}`