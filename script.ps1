# az login --use-device-code


# set -e # exit if error
# Create an App Service app with deployment from GitHub
# Variable block
$randomIdentifier=Get-Random
$location="East US"
$resourceGroup="tacaspi-rg8"
$tag="deploy-github"
$gitrepo="https://github.com/tamirc3/parkinglotCloud" 
$appServicePlan="msdocs-app-service-plan-$randomIdentifier"
$webapp="msdocs-web-app-$randomIdentifier"

# Create a resource group.
echo "Creating $resourceGroup in "$location"..."
az group create --name $resourceGroup --location "$location" --tag $tag

# Create an App Service plan in `FREE` tier.
echo "Creating $appServicePlan"
az appservice plan create --name $appServicePlan --resource-group $resourceGroup --sku FREE

# Create a web app.
echo "Creating $webapp"
az webapp create --name $webapp --resource-group $resourceGroup --plan $appServicePlan 


# Deploy code from a public GitHub repository. 
az webapp deployment source config --name $webapp --resource-group $resourceGroup `
--repo-url $gitrepo --branch main --manual-integration --debug

echo $webapp

# Use curl to see the web app.
$site="http://$webapp.azurewebsites.net/weatherforecast"
echo $site
curl "$site" # Optionally, copy and paste the output of the previous command into a browser to see the web app