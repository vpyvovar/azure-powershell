# ----------------------------------------------------------------------------------
#
# Copyright Microsoft Corporation
# Licensed under the Apache License, Version 2.0 (the "License");
# you may not use this file except in compliance with the License.
# You may obtain a copy of the License at
# http://www.apache.org/licenses/LICENSE-2.0
# Unless required by applicable law or agreed to in writing, software
# distributed under the License is distributed on an "AS IS" BASIS,
# WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
# See the License for the specific language governing permissions and
# limitations under the License.
# ----------------------------------------------------------------------------------

<#
	.SYNOPSIS
	Tests for checking virtual network rule core functionalities. This includes - create, update, delete, list and get operations
#>
function Test-GetInterfaceEndpointProfile
{
	# Setup
	$location = "East US 2 EUAP"
	$rg = Create-ResourceGroupForTest $location
	$server1 = Create-ServerForTest $rg $location

	$InterfaceEndpointProfileName1 = Get-InterfaceEndpointProfileName
	$vnetName1 = "vnet1"
	$virtualNetwork1 = CreateAndGetVirtualNetwork $rg $vnetName1 $location
	$virtualNetworkSubnetId1 = $virtualNetwork1.Subnets[0].Id

	try
	{
		# Create rule 1
		$InterfaceEndpointProfile1 = New-AzureRmSqlServerInterfaceEndpointProfile -ResourceGroupName $rg.ResourceGroupName -ServerName $server1.ServerName `
		-InterfaceEndpointProfileName $InterfaceEndpointProfileName1 -VirtualNetworkSubnetId $virtualNetworkSubnetId1 
		Assert-AreEqual $InterfaceEndpointProfile1.ServerName $server1.ServerName
		Assert-AreEqual $InterfaceEndpointProfile1.InterfaceEndpointProfileName $InterfaceEndpointProfileName1
		Assert-AreEqual $InterfaceEndpointProfile1.VirtualNetworkSubnetId $virtualNetworkSubnetId1

		# Get rule 1
		$resp = Get-AzureRmSqlServerInterfaceEndpointProfile -ResourceGroupName $rg.ResourceGroupName -ServerName $server1.ServerName -InterfaceEndpointProfileName $InterfaceEndpointProfileName1
		Assert-AreEqual $resp.VirtualNetworkSubnetId $virtualNetworkSubnetId1
	}
	finally
	{
		# Clean the enviroment
		Remove-AzureRmSqlServerInterfaceEndpointProfile -ResourceGroupName $rg.ResourceGroupName -ServerName $server1.ServerName -InterfaceEndpointProfileName $InterfaceEndpointProfileName1
		Remove-ResourceGroupForTest $rg
	}
}

<#
	.SYNOPSIS
	Tests Getting a interface endpoint profile
#>
function Test-ListByServerInterfaceEndpointProfile 
{
	# Setup
	$location = "East US 2 EUAP"
	$rg = Create-ResourceGroupForTest $location
	$server1 = Create-ServerForTest $rg $location

	$InterfaceEndpointProfileName1 = Get-InterfaceEndpointProfileName
	$vnetName1 = "vnet1"
	$virtualNetwork1 = CreateAndGetVirtualNetwork $rg $vnetName1 $location
	$virtualNetworkSubnetId1 = $virtualNetwork1.Subnets[0].Id

	try
	{
		# Create rule 1
		$InterfaceEndpointProfile1 = New-AzureRmSqlServerInterfaceEndpointProfile -ResourceGroupName $rg.ResourceGroupName -ServerName $server1.ServerName `
		-InterfaceEndpointProfileName $InterfaceEndpointProfileName1 -VirtualNetworkSubnetId $virtualNetworkSubnetId1 
		Assert-AreEqual $InterfaceEndpointProfile1.ServerName $server1.ServerName
		Assert-AreEqual $InterfaceEndpointProfile1.InterfaceEndpointProfileName $InterfaceEndpointProfileName1
		Assert-AreEqual $InterfaceEndpointProfile1.VirtualNetworkSubnetId $virtualNetworkSubnetId1

		# Get list of rules
		$resp = Get-AzureRmSqlServerInterfaceEndpointProfile -ResourceGroupName $rg.ResourceGroupName -ServerName $server1.ServerName
		Assert-AreEqual $resp.Count 1
	}
	finally
	{
		# Clean the enviroment
		Remove-AzureRmSqlServerInterfaceEndpointProfile -ResourceGroupName $rg.ResourceGroupName -ServerName $server1.ServerName -InterfaceEndpointProfileName $InterfaceEndpointProfileName1
		Remove-ResourceGroupForTest $rg
	}
}

<#
	.SYNOPSIS
	Tests Removing interface endpoint
#>
function Test-RemoveInterfaceEndpointProfile
{
	# Setup
	$location = "East US 2 EUAP"
	$rg = Create-ResourceGroupForTest $location

	$InterfaceEndpointProfileName = Get-InterfaceEndpointProfileName
	$vnetName = "vnet1"
	$virtualNetwork = CreateAndGetVirtualNetwork $rg $vnetName $location
	$virtualNetworkSubnetId = $virtualNetwork.Subnets[0].Id

	$server = Create-ServerForTest $rg $location

	try
	{
		# Create rule
		$InterfaceEndpointProfile = New-AzureRmSqlServerInterfaceEndpointProfile -ResourceGroupName $rg.ResourceGroupName -ServerName $server.ServerName `
		-InterfaceEndpointProfileName $InterfaceEndpointProfileName -VirtualNetworkSubnetId $virtualNetworkSubnetId
		Assert-AreEqual $InterfaceEndpointProfile.ServerName $server.ServerName
		Assert-AreEqual $InterfaceEndpointProfile.InterfaceEndpointProfileName $InterfaceEndpointProfileName
		Assert-AreEqual $InterfaceEndpointProfile.VirtualNetworkSubnetId $virtualNetworkSubnetId

		# Remove rule and check if rule is deleted
		$job = Remove-AzureRmSqlServerInterfaceEndpointProfile -ResourceGroupName $rg.ResourceGroupName -ServerName $server.ServerName `
			-InterfaceEndpointProfileName $InterfaceEndpointProfileName -AsJob
		$job | Wait-Job
		$resp = $job.Output

		$all = Get-AzureRmSqlServerInterfaceEndpointProfile -ResourceGroupName $rg.ResourceGroupName -ServerName $server.ServerName
		Assert-AreEqual $all.Count 0
	}
	finally
	{
		Remove-ResourceGroupForTest $rg
	}
}

<#
	.SYNOPSIS
	Create a virtual network
#>
function CreateAndGetVirtualNetwork ($resourceGroup, $vnetName, $location = "eastus2euap")
{
	$subnetName = "Public"

	$addressPrefix = "10.0.0.0/24"
	$delegation = New-AzureRmDelegation -Name "sqlDelegation" -ServiceName "Microsoft.Sql/servers"

	$subnet = New-AzureRmVirtualNetworkSubnetConfig -Name $subnetName -AddressPrefix $addressPrefix -delegation $delegation
	$vnet = New-AzureRmvirtualNetwork -Name $vnetName -ResourceGroupName $resourceGroup.ResourceGroupName -Location $location -AddressPrefix 10.0.0.0/16 -Subnet $subnet

	$getVnet = Get-AzureRmVirtualNetwork -Name $vnetName -ResourceGroupName $resourceGroup.ResourceGroupName

	return $getVnet
}
