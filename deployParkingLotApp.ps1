az login --use-device-code

# # see which avilable locations we have
$locations = az account list-locations | ConvertFrom-Json
$choosenLocation =  $locations[0].name
echo $choosenLocation

$randomIdentifier= "1" #Get-Random // can add this flag to generate a unique name each deployment
$location=$choosenLocation
$resourceGroup="parkinglot-rg"
$tag="deploy-github"
$gitrepo="https://github.com/tamirc3/parkinglotCloud" 
$appServicePlan="parkinglot-app-service-plan-$randomIdentifier"
$webapp="parkinglot-web-app-$randomIdentifier"

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
--repo-url $gitrepo --branch main --manual-integration #--debug //add this flag for addional information

echo $webapp

# Use curl to see the web app.
$site="https://$webapp.azurewebsites.net/health"
echo $site
curl "$site"

# Use curl to see the web app.
$site="https://$webapp.azurewebsites.net/swagger"
echo $site
curl "$site"  