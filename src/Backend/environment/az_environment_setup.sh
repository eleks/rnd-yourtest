#!/bin/bash

resourceGroup="%0"
clientId="%1"
issuer="%2"

az group deployment create \
  --name $resourceGroup"_Deployment" \
  --resource-group $resourceGroup \
  --template-file azure-yourtest-template.json \
  --parameters site_name=$resourceGroup \
  --parameters ad_clientId=$clientId \
  --parameters ad_issuer=$issuer