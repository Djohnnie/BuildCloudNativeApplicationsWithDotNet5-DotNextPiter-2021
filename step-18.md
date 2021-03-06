# Deploy the WorkerService to AKS and make it call the WebApi service

[Previous step](step-17.md) - [Next step](step-19.md)

Create a new file called deployment-workerservice.yaml in Visual Studio Code and use the following content:

```yaml
apiVersion: apps/v1
kind: Deployment
metadata:
  name: workerservice
spec:
  replicas: 1
  selector:
    matchLabels:
      app: workerservice
  template:
    metadata:
      labels:
        app: workerservice
    spec:
      containers:
      - name: workerservice
        image: djohnniekefordotnext.azurecr.io/workerservice:latest
        env:
            - name: WebApiServiceUri
              value: "http://webapi"
        resources:
          limits:
            memory: "128Mi"
            cpu: "250m"
        ports:
        - containerPort: 80
```

Apply the deployment script by using the Visual Studio Code command palette, find the Pods in the Kubernetes workloads and follow its logs:

![dotnet new](images/sshot-90.png)

[Previous step](step-17.md) - [Next step](step-19.md)