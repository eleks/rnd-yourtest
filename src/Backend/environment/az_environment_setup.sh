#!/bin/bash

resourceGroup=$1
siteName=$2
clientId=$3
issuer=$4

az group deployment create \
  --name $resourceGroup"_Deployment" \
  --resource-group $resourceGroup \
  --template-file azure-yourtest-template.json \
  --parameters site_name=$siteName \
  --parameters ad_clientId=$clientId \
  --parameters ad_issuer=$issuer \
  --debug