# Add an NGINX ingress controller to AKS

[Previous step](step-20.md) - [Next step](step-22.md)

Create an Ingress namespace and change to it:

```yaml
apiVersion: v1
kind: Namespace
metadata:
  name: ingress
  resourceVersion: "144"
spec:
  finalizers:
  - kubernetes
status:
  phase: Active
```

Use Helm to install the NGINX ingress into your Kubernetes cluster. First update your local Helm repository:

```
helm repo add ingress-nginx https://kubernetes.github.io/ingress-nginx
```

```
helm repo update
```

Use your own created IP address:

```
helm install nginx-ingress ingress-nginx/ingress-nginx --namespace ingress --set controller.replicaCount=2 --set controller.nodeSelector."beta\.kubernetes\.io/os"=linux --set defaultBackend.nodeSelector."beta\.kubernetes\.io/os"=linux --set controller.admissionWebhooks.patch.nodeSelector."beta\.kubernetes\.io/os"=linux --set controller.service.loadBalancerIP="<your ip address>"
```

```
helm install nginx-ingress ingress-nginx/ingress-nginx --namespace ingress --set controller.replicaCount=2 --set controller.nodeSelector."beta\.kubernetes\.io/os"=linux --set defaultBackend.nodeSelector."beta\.kubernetes\.io/os"=linux --set controller.admissionWebhooks.patch.nodeSelector."beta\.kubernetes\.io/os"=linux --set controller.service.loadBalancerIP="51.138.58.63"
```

Your DNS should now lead you to a NGINX 404 NOT FOUND page:

![dotnet new](images/sshot-99.png)

Return to the dotnext namespace, add an Ingress controller for the web service and use your DNS you have created before:

```yaml
apiVersion: extensions/v1beta1
kind: Ingress
metadata:
  name: web-ingress
  annotations:
    kubernetes.io/ingress.class: nginx
    nginx.ingress.kubernetes.io/rewrite-target: /$1
spec:
  rules:
  - host: cloudnative-dotnext-web.westeurope.cloudapp.azure.com
    http:
      paths:
      - backend:
          serviceName: web
          servicePort: 80
        path: /(.*)
```

[Previous step](step-20.md) - [Next step](step-22.md)