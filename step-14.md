# Run your Docker containers inside Azure App Service from ACR

[Previous step](step-13.md) - [Next step](step-15.md)

After the images have been pushed to your Azure Container Registry and the admin account has been enabled, you are able to deploy the Docker containers inside an App Service.

Open the details of your Docker images inside your ACR and click the "Deploy to web app" context-menu option:

![dotnet new](images/sshot-65.png)

![dotnet new](images/sshot-66.png)

![dotnet new](images/sshot-67.png)

Do the same thing for the WorkerService Docker image:

![dotnet new](images/sshot-68.png)

![dotnet new](images/sshot-69.png)

![dotnet new](images/sshot-70.png)

The configuration for the WorkerService should contain the WebApiServiceUri. You can do this using the Configuration page inside the Azure App Service:

![dotnet new](images/sshot-71.png)

Now you can use the Advanced Tools page inside the Azure App Service to look at the logs from the Worker Service:

![dotnet new](images/sshot-72.png)

![dotnet new](images/sshot-73.png)

[Previous step](step-13.md) - [Next step](step-15.md)