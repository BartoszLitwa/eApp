run in `/src/` directory

# Docker Platform Service
`docker build -t bartoszlitwa/eapp.platformservice.api -f PlatformService\Dockerfile .`

`docker push bartoszlitwa/eapp.platformservice.api`

# Docker Command Service
`docker build -t bartoszlitwa/eapp.commandservice.api -f CommandService/Dockerfile .`

`docker push bartoszlitwa/eapp.commandservice.api`

# Hosts Mapping
C:\Windows\System32\drivers\etc\hosts
Map to custom domain locally
127.0.0.1 eapp.com

# Kubectl
`kubectl get deployment`

`kubectl get pods`

`kubectl get services`

`kubectl delete pod {pod-name}`

`kubectl apply -f {file}`

`kubectl delete deployment {deployment-name}`

`kubectl rollout restart deployment {deployment-name}`

`kubectl get storageclass`

`kubectl get pvc`

`kubectl create secret generic mssql --from-literal={secret-name}="{secret-value}"`
`kubectl create secret generic mssql --from-literal=SA_PASSWORD="admin123!"`

`kubectl get secret`

`kubectl delete secret {secret-name}`