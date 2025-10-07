// -----------------------------------------------------------------------------
// Parameters
// -----------------------------------------------------------------------------
@description('Sql admin password.')
@minLength(36)
@secure()
param sqlAdminPass string

@description('A map of { "name": guid } pairs')
@secure()
param aadGroups object

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
module appConfigDeploy '../../common-bicep/integration/app-config.bicep' = {
  name: 'appConfigDeploy'
  params: {
    disableLocalAccess: true
    suffix: suffix
    location: location
    tags: tags
  }
}

module sqlServerDeploy '../../common-bicep/database/sqldb-server.bicep' = {
  name: 'sqlServerDeploy'
  params: {
    adminLogin: 'shared_infra'
    adminPassword: sqlAdminPass
    adminAadGroup: {
      name: 'repos'
      sid: aadGroups.repos
    }
    suffix: suffix
    location: location
    tags: tags
  }
}

module sqlServerDbDeploy '../../common-bicep/database/sqldb.bicep' = {
  name: 'sqlServerDbDeploy'
  params: {
    useFree: true
    databaseName: 'DojoDB'
    sqlServerResourceName: sqlServerDeploy.outputs.resourceName
    location: location
    tags: tags
  }
}

// -----------------------------------------------------------------------------
// Permissions
// -----------------------------------------------------------------------------
module assignAppConfigOwner '../../common-bicep/security/sp-assign-rg-role.bicep' = {
  name: 'assignAppConfigOwner'
  params: {
    role: 'App Configuration Data Owner'
    principalIds: [aadGroups.admins, aadGroups.repos]
    principalType: 'Group'
  }
}

module assignAppConfigRead '../../common-bicep/security/sp-assign-rg-role.bicep' = {
  name: 'assignAppConfigRead'
  params: {
    role: 'App Configuration Data Reader'
    principalIds: [aadGroups.engineers]
    principalType: 'Group'
  }
}

// -----------------------------------------------------------------------------
// Output
// -----------------------------------------------------------------------------
output sqlConnection string = sqlServerDbDeploy.outputs.connectionString
