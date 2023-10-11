# CICD build .NET Core and deploy to Azure App

## 1. Create workflows

✨ In the GitHub Repository, create the .github/workflows/ folder and create the *.yml file inside

![Untitled](CICD%20build%20NET%20Core%20and%20deploy%20to%20Azure%20App%20e3cec8e1d7274c0eb12d8b3d6166aedc/Untitled.png)

[⚙️](https://www.bing.com/ck/a?!&&p=9c5d147000f50c8dJmltdHM9MTY5Njk4MjQwMCZpZ3VpZD0yOTBhYzhlMS01NDk3LTY5M2EtMTg4NS1kYjhmNTU5NjY4OTQmaW5zaWQ9NTIxNA&ptn=3&hsh=3&fclid=290ac8e1-5497-693a-1885-db8f55966894&psq=setting+emoji&u=a1aHR0cHM6Ly9lbW9qaXBlZGlhLm9yZy9nZWFy&ntb=1) **Step 1:  Setup and build .NET**

✨ in main.yml file:

```yaml
name: Publish

on:
  workflow_dispatch:
  push:
    branches:
      - main

jobs:
  publish:
    runs-on: ubuntu-latest

    steps:
      - uses: actions/checkout@v3

      - name: Setup .NET
        uses: actions/setup-dotnet@v3
        with:
          dotnet-version: '6.0.x'

      - name: Restore
        run: dotnet restore ./DeployAzureExample/DeployAzureExample.sln

      - name: Build
        run: dotnet build ./DeployAzureExample/DeployAzureExample.sln --configuration Release --no-restore

      - name: Publish
        run: dotnet publish ./DeployAzureExample/DeployAzureExample.sln --configuration Release --no-build
```

[⚙️](https://www.bing.com/ck/a?!&&p=9c5d147000f50c8dJmltdHM9MTY5Njk4MjQwMCZpZ3VpZD0yOTBhYzhlMS01NDk3LTY5M2EtMTg4NS1kYjhmNTU5NjY4OTQmaW5zaWQ9NTIxNA&ptn=3&hsh=3&fclid=290ac8e1-5497-693a-1885-db8f55966894&psq=setting+emoji&u=a1aHR0cHM6Ly9lbW9qaXBlZGlhLm9yZy9nZWFy&ntb=1) **Step 2:  Create Azure app services**

✨ **Access**: [https://portal.azure.com/](https://portal.azure.com/) → **App Services**

![Untitled](CICD%20build%20NET%20Core%20and%20deploy%20to%20Azure%20App%20e3cec8e1d7274c0eb12d8b3d6166aedc/Untitled%201.png)

✨ **Create Web app →**

![Untitled](CICD%20build%20NET%20Core%20and%20deploy%20to%20Azure%20App%20e3cec8e1d7274c0eb12d8b3d6166aedc/Untitled%202.png)

✨ **After successful creation, go to the overview tab -> download publish profile to load the PublishSettings file**

![Untitled](CICD%20build%20NET%20Core%20and%20deploy%20to%20Azure%20App%20e3cec8e1d7274c0eb12d8b3d6166aedc/Untitled%203.png)

[⚙️](https://www.bing.com/ck/a?!&&p=9c5d147000f50c8dJmltdHM9MTY5Njk4MjQwMCZpZ3VpZD0yOTBhYzhlMS01NDk3LTY5M2EtMTg4NS1kYjhmNTU5NjY4OTQmaW5zaWQ9NTIxNA&ptn=3&hsh=3&fclid=290ac8e1-5497-693a-1885-db8f55966894&psq=setting+emoji&u=a1aHR0cHM6Ly9lbW9qaXBlZGlhLm9yZy9nZWFy&ntb=1) **Step 3: Continue setting up workflows for Github Actions to deploy code to Azure App Services**

```yaml
name: Publish

on:
  workflow_dispatch:
  push:
    branches:
      - main

env:
  AZURE_WEB_APP_NAME: testtimeapi
  AZURE_WEB_APP_PACKAGE_PATH: './publish'
  

jobs:
  publish:
    runs-on: ubuntu-latest

    steps:
      - uses: actions/checkout@v3

      - name: Setup .NET
        uses: actions/setup-dotnet@v3
        with:
          dotnet-version: '6.0.x'

      - name: Restore
        run: dotnet restore ./DeployAzureExample/DeployAzureExample.sln

      - name: Build
        run: dotnet build ./DeployAzureExample/DeployAzureExample.sln --configuration Release --no-restore

      - name: Publish
        run: dotnet publish ./DeployAzureExample/DeployAzureExample.sln --configuration Release --no-build --output '${{ env.AZURE_WEB_APP_PACKAGE_PATH }}'
      
      - name: Deployment
        uses: azure/webapps-deploy@v2
        with:
          app-name: ${{ env.AZURE_WEB_APP_NAME }}
          publish-profile: ${{ secrets.AZURE_PUBLISH_PROFILE }}
          package: ${{ env.AZURE_WEB_APP_PACKAGE_PATH }}
```

⚠️ I**ncluding the env variable and the secrets variable located in the settings of the repository**

![Untitled](CICD%20build%20NET%20Core%20and%20deploy%20to%20Azure%20App%20e3cec8e1d7274c0eb12d8b3d6166aedc/Untitled%204.png)

![Untitled](CICD%20build%20NET%20Core%20and%20deploy%20to%20Azure%20App%20e3cec8e1d7274c0eb12d8b3d6166aedc/Untitled%205.png)

![Untitled](CICD%20build%20NET%20Core%20and%20deploy%20to%20Azure%20App%20e3cec8e1d7274c0eb12d8b3d6166aedc/Untitled%206.png)

**💅 DONE !**

**🧑‍💻 Try changing the code in the main branch to test the action**

![Untitled](CICD%20build%20NET%20Core%20and%20deploy%20to%20Azure%20App%20e3cec8e1d7274c0eb12d8b3d6166aedc/Untitled%207.png)

**🧑‍💻 Visit the Default domain on azure to see if the .NET Core App is working**

![Untitled](CICD%20build%20NET%20Core%20and%20deploy%20to%20Azure%20App%20e3cec8e1d7274c0eb12d8b3d6166aedc/Untitled%208.png)

### Reference: [How To Build a CI/CD Pipeline With GitHub Actions And .NET (milanjovanovic.tech)](https://www.milanjovanovic.tech/blog/how-to-build-ci-cd-pipeline-with-github-actions-and-dotnet)