// -----------------------------------------------------------------------------
// Parameters
// -----------------------------------------------------------------------------
@description('The path of the API container image.')
param apiContainerImage string

@description('The resource location.')
@minLength(3)
param location string = resourceGroup().location ?? ''

// -----------------------------------------------------------------------------
// Variables
// -----------------------------------------------------------------------------
@description('The workload name.')
var workload = split(resourceGroup().name, '-')[1]

@description('The short location suffix.')
var locationShort = split(resourceGroup().name, '-')[2]

@description('Amalgam of the workload and the (short) location name.')
var suffix = trim('${workload}-${locationShort}')

@description('The resource tags.')
var tags = resourceGroup().tags

// -----------------------------------------------------------------------------
// Resources
// -----------------------------------------------------------------------------
module uiStaticWebAppDeploy '../../common-bicep/web/static-web-app.bicep' = {
  name: 'uiStaticWebAppDeploy'
  params: {
    shortName: ''
    prefix: 'ui'
    suffix: suffix
    location: 'westeurope' // uksouth not supported at present
    tags: tags
  }
}

module apiAppServicePlanDeploy '../../common-bicep/web/app-service-plan.bicep' = {
  name: 'apiAppServicePlanDeploy'
  params: {
    prefix: 'api'
    suffix: suffix
    location: location
    tags: tags
  }
}

module apiAppServiceDeploy '../../common-bicep/web/app-service.bicep' = {
  name: 'apiAppServiceDeploy'
  params: {
    appServicePlanId: apiAppServicePlanDeploy.outputs.resourceId
    containerUrl: apiContainerImage
    healthCheckPath: '/healthz'
    appSettings: [
      {
        name: 'AllowedHosts'
        value: 'api-app-${suffix}.azurewebsites.net'
      }
      {
        name: 'Cors__Origins__0'
        value: 'https://opsdojo.ne1410s.co.uk'
      }
      {
        name: 'Cors__Origins__1'
        value: uiStaticWebAppDeploy.outputs.appUrl
      }
    ]
    connectionStrings: [
      {
        name: 'PortalDb'
        value: 'Server=tcp:sql-commoninfra-uks${environment().suffixes.sqlServerHostname},1433;Initial Catalog=DojoDB;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;Authentication="Active Directory Default";'
      }
    ]
    shortName: ''
    prefix: 'api'
    suffix: suffix
    location: location
    tags: tags
  }
}

// -----------------------------------------------------------------------------
// Output
// -----------------------------------------------------------------------------
output apiAppUrl string = apiAppServiceDeploy.outputs.appUrl
output apiPrincipalId string = apiAppServiceDeploy.outputs.resourcePrincipalId
output uiAppUrl string = uiStaticWebAppDeploy.outputs.appUrl
output uiSwaName string = uiStaticWebAppDeploy.outputs.resourceName
