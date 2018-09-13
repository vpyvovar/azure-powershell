---
external help file: Microsoft.Azure.Commands.Sql.dll-Help.xml
Module Name: AzureRM.Sql
online version: https://docs.microsoft.com/en-us/powershell/module/azurerm.sql/set-azurermsqlserverinterfaceendpointprofile
schema: 2.0.0
---

# Set-AzureRmSqlServerInterfaceEndpointProfile

## SYNOPSIS
Modifies the configuration of an Azure SQL Server Interface endpoint profile.

## SYNTAX

```
Set-AzureRmSqlServerInterfaceEndpointProfile -InterfaceEndpointProfileName <String> -VirtualNetworkSubnetId <String>
 [-AsJob] -ServerName <String> [-ResourceGroupName] <String>
 [-DefaultProfile <IAzureContextContainer>] [-WhatIf] [-Confirm] [<CommonParameters>]
```

## DESCRIPTION
This command modifies the configuration of an Azure SQL Server Interface endpoint profile.
To control the set of interface endpoint profiles in the server, use 'Add-AzureRmSqlServerInterfaceEndpointProfile' and 'Remove-AzureRmSqlServerInterfaceEndpointProfile' instead.

## EXAMPLES

### Example 1
```
PS C:\> $interfaceEndpointProfile = Set-AzureRmSqlServerInterfaceEndpointProfile -ResourceGroupName rg -ServerName serverName -InterfaceEndpointProfileName interfaceEndpointProfileName -VirtualNetworkSubnetId virtualNetworkSubnetId
```

Modifies an existing interface endpoint profile with the new virtual network subnet id which contains information about the new virtual network

## PARAMETERS

### -AsJob
Run cmdlet in the background

```yaml
Type: SwitchParameter
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -DefaultProfile
The credentials, account, tenant, and subscription used for communication with azure

```yaml
Type: IAzureContextContainer
Parameter Sets: (All)
Aliases: AzureRmContext, AzureCredential

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

```yaml
Type: SwitchParameter
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -ResourceGroupName
The name of the resource group.

```yaml
Type: String
Parameter Sets: (All)
Aliases:

Required: True
Position: 0
Default value: None
Accept pipeline input: True (ByPropertyName)
Accept wildcard characters: False
```

### -ServerName
The Azure Sql Server name.

```yaml
Type: String
Parameter Sets: (All)
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -InterfaceEndpointProfileName
The name of the Azure Sql Server Interface Endpoint Profile.

```yaml
Type: String
Parameter Sets: (All)
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -VirtualNetworkSubnetId
The Virtual Network Subnet Id for the interface endpoint profile.

```yaml
Type: String
Parameter Sets: (All)
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Confirm
Prompts you for confirmation before running the cmdlet.

```yaml
Type: SwitchParameter
Parameter Sets: (All)
Aliases: cf

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -WhatIf
Shows what would happen if the cmdlet runs.
The cmdlet is not run.

```yaml
Type: SwitchParameter
Parameter Sets: (All)
Aliases: wi

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### CommonParameters
This cmdlet supports the common parameters: -Debug, -ErrorAction, -ErrorVariable, -InformationAction, -InformationVariable, -OutVariable, -OutBuffer, -PipelineVariable, -Verbose, -WarningAction, and -WarningVariable. For more information, see about_CommonParameters (http://go.microsoft.com/fwlink/?LinkID=113216).

## INPUTS

### System.String

## OUTPUTS

### Microsoft.Azure.Commands.Sql.InterfaceEndpointProfile.Model.AzureSqlServerInterfaceEndpointProfileModel

## NOTES

## RELATED LINKS
