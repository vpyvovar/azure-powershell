---
external help file: Microsoft.Azure.Commands.Sql.dll-Help.xml
Module Name: AzureRM.Sql
online version: https://docs.microsoft.com/en-us/powershell/module/azurerm.sql/get-azurermsqlserverinterfaceendpointprofile
schema: 2.0.0
---

# Get-AzureRmSqlServerInterfaceEndpointProfile

## SYNOPSIS
Gets or lists Azure SQL Server Server Interface endpoint profile.

## SYNTAX

```
Get-AzureRmSqlServerInterfaceEndpointProfile [-InterfaceEndpointProfileName <String>] -ServerName <String>
 [-ResourceGroupName] <String> [-DefaultProfile <IAzureContextContainer>] [<CommonParameters>]
```

## DESCRIPTION
This command gets a specific Azure SQL Server Interface endpoint profile or a list of Azure SQL Server Interface endpoint profiles under a server.

## EXAMPLES

### Example 1
```
PS C:\> $interfaceEndpointProfile = Get-AzureRmSqlServerInterfaceEndpointProfile -ResourceGroupName rg -ServerName serverName -InterfaceEndpointProfileName interfaceEndpointProfile
```

Gets or lists Azure SQL Server Server Interface endpoint profile

### Example 2
```
PS C:\> $interfaceEndpointProfiles = Get-AzureRmSqlServerInterfaceEndpointProfile -ResourceGroupName rg -ServerName serverName
```

Gets the list of Azure SQL Server Interface endpoint profiles under the specified server

## PARAMETERS

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
The Azure Sql Server Interface Endpoint Profile name.

```yaml
Type: String
Parameter Sets: (All)
Aliases:

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
