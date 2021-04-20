# BuildCloudNativeApplicationsWithDotNet5-DotNextPiter-2021
Repository containing guidance and code to use during the DotNext Piter 2021 workshop: "Building Cloud Native applications with .NET 5 and AKS"

## Presentation

[Presentation Slides](DOTNEXT-Building-Cloud-Native-Applications-with-DOTNET-and-AKS.pdf)

## Prerequisites

Please prepare your system and install the following software:

- [.NET 5 SDK 5.0.202](https://dotnet.microsoft.com/download/dotnet/5.0)
- [Visual Studio Code](https://code.visualstudio.com/)
- [Azure CLI](https://docs.microsoft.com/en-us/cli/azure/install-azure-cli-windows?tabs=azure-cli)
- [Docker Desktop](https://www.docker.com/products/docker-desktop)
- [Helm](https://github.com/helm/helm/releases)

Additionally, prepare the following settings and extensions:

### Docker Desktop

Prepare the following settings:

![Screen capture from Docker Desktop Kubernetes settings](sshot-01.png)

### Visual Studio Code

Install the following extensions:

- [YAML](https://marketplace.visualstudio.com/items?itemName=redhat.vscode-yaml)
- [C#](https://marketplace.visualstudio.com/items?itemName=ms-dotnettools.csharp)
- [dotnet](https://marketplace.visualstudio.com/items?itemName=leo-labs.dotnet)
- [NuGet Package Manager](https://marketplace.visualstudio.com/items?itemName=jmrog.vscode-nuget-package-manager)
- [Azure Account](https://marketplace.visualstudio.com/items?itemName=ms-vscode.azure-account)
- [Azure CLI Tools](https://marketplace.visualstudio.com/items?itemName=ms-vscode.azurecli)
- [Docker](https://marketplace.visualstudio.com/items?itemName=ms-azuretools.vscode-docker)
- [Kubernetes](https://marketplace.visualstudio.com/items?itemName=ms-kubernetes-tools.vscode-kubernetes-tools)
- [Kubernetes Support](https://marketplace.visualstudio.com/items?itemName=ipedrazas.kubernetes-snippets)
- [Azure Kubernetes Service](https://marketplace.visualstudio.com/items?itemName=ms-kubernetes-tools.vscode-aks-tools)

## Workshop

### Part 1 - Cloud Native with .NET 5

- [Step 01](step-01.md) - Create a .NET 5 HTTP Service (ASP.NET WebApi).
- [Step 02](step-02.md) - Create a .NET 5 Worker Service that calls the HTTP Service.
- [Step 03](step-03.md) - Test your Worker Service and HTTP Service using Project Tye.
- [Step 04](step-04.md) - Project Tye and custom configuration.
- [Step 05](step-05.md) - Create a .NET 5 gRPC Service and Client.
- [Step 06](step-06.md) - Changing the gRPC Service and Client to use a common part.
- [Step 07](step-07.md) - Putting everything together with custom configuration.

### Part 2 - Containerize your services with Docker

- [Step 08](step-08.md) - Project Tye and additional Docker containers.
- [Step 09](step-09.md) - Create your own Docker images and run them as local containers.
- [Step 10](step-10.md) - Connect Visual Studio Code to your Azure Subscription.
- [Step 11](step-11.md) - Create a resource group in your Azure subscription.
- [Step 12](step-12.md) - Create the ACR (Azure Container Registry) resource in your Azure subscription.
- [Step 13](step-13.md) - Connect Visual Studio Code to your ACR and push your Docker images.
- [Step 14](step-14.md) - Run your Docker containers inside Azure App Service from ACR.

### Part 3 - Deploy your containerized services on AKS

- [Step 15](step-15.md) - Create the AKS (Azure Kubernetes Service) resource in your Azure subscription.
- [Step 16](step-16.md) - Allow AKS to access ACR to automatically pull Docker images.
- [Step 17](step-17.md) - Deploy the WebApi to AKS and create a service for it.
- [Step 18](step-18.md) - Deploy the WorkerService to AKS and make it call the WebApi service.
- [Step 19](step-19.md) - Create an ASP.NET Core MVC Web application and deploy it to AKS.
- [Step 20](step-20.md) - Create a public IP-address and DNS entry.
- [Step 21](step-21.md) - Add an NGINX ingress controller to AKS.
- [Step 22](step-22.md) - Have some fun with the web application!