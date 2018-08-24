// ----------------------------------------------------------------------------------
//
// Copyright Microsoft Corporation
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// http://www.apache.org/licenses/LICENSE-2.0
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// ----------------------------------------------------------------------------------

using Microsoft.Azure.Commands.Common.Authentication.Abstractions;
using Microsoft.Azure.Commands.Sql.InterfaceEndpointProfile.Model;
using Microsoft.Azure.Commands.Sql.InterfaceEndpointProfile.Services;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.Azure.Commands.Sql.InterfaceEndpointProfile.Adapter
{
    /// <summary>
    /// Adapter for Interface Endpoint Profile operations
    /// </summary>
    public class AzureSqlServerInterfaceEndpointProfileAdapter
    {
        /// <summary>
        /// Gets or sets the AzureEndpointsCommunicator which has all the needed management clients
        /// </summary>
        private AzureSqlServerInterfaceEndpointProfileCommunicator Communicator { get; set; }

        /// <summary>
        /// Gets or sets the Azure profile
        /// </summary>
        public IAzureContext Context { get; set; }

        /// <summary>
        /// Constructs a Interface Endpoint Profile adapter
        /// </summary>
        /// <param name="profile">The current azure profile</param>
        /// <param name="subscription">The current azure subscription</param>
        public AzureSqlServerInterfaceEndpointProfileAdapter(IAzureContext context)
        {
            Context = context;
            Communicator = new AzureSqlServerInterfaceEndpointProfileCommunicator(Context);
        }

        /// <summary>
        /// Gets a Interface Endpoint Profile in a server
        /// </summary>
        /// <param name="resourceGroupName">The name of the resource group</param>
        /// <param name="serverName">The name of the server</param>
        /// <param name="InterfaceEndpointProfileName">The Interface Endpoint Profile name</param>
        /// <returns>The Interface Endpoint Profile</returns>
        public AzureSqlServerInterfaceEndpointProfileModel GetInterfaceEndpointProfile(string resourceGroupName, string serverName, string InterfaceEndpointProfileName)
        {
            var resp = Communicator.Get(resourceGroupName, serverName, InterfaceEndpointProfileName);
            return CreateInterfaceEndpointProfileModelFromResponse(resourceGroupName, serverName, resp);
        }

        /// <summary>
        /// Gets a list of all the Interface Endpoint Profiles in a server
        /// </summary>
        /// <param name="resourceGroupName">The name of the resource group</param>
        /// <param name="serverName">The name of the server</param>
        /// <returns>A list of all the Interface Endpoint Profiles</returns>
        public List<AzureSqlServerInterfaceEndpointProfileModel> ListInterfaceEndpointProfiles(string resourceGroupName, string serverName)
        {
            var resp = Communicator.List(resourceGroupName, serverName);
            return resp.Select((s) =>
            {
                return CreateInterfaceEndpointProfileModelFromResponse(resourceGroupName, serverName, s);
            }).ToList();
        }

        /// <summary>
        /// Upserts a Interface Endpoint Profile
        /// </summary>
        /// <param name="resourceGroup">The name of the resource group</param>
        /// <param name="serverName">The name of ther server</param>
        /// <param name="model">The Interface Endpoint Profile to create</param>
        /// <returns>The updated Interface Endpoint Profile model</returns>
        public AzureSqlServerInterfaceEndpointProfileModel UpsertInterfaceEndpointProfile(AzureSqlServerInterfaceEndpointProfileModel model)
        {
            var resp = Communicator.CreateOrUpdate(model.ResourceGroupName, model.ServerName, model.InterfaceEndpointProfileName, new Management.Sql.Models.InterfaceEndpointProfile()
            {
                VirtualNetworkSubnetId = model.VirtualNetworkSubnetId,                
            });
            return CreateInterfaceEndpointProfileModelFromResponse(model.ResourceGroupName, model.ServerName, resp);
        }

        /// <summary>
        /// Deletes a Interface Endpoint Profile
        /// </summary>
        /// <param name="resourceGroupName">The resource group the server is in</param>
        /// <param name="serverName">The name of the server from which the Interface Endpoint Profile should be removed</param>
        /// <param name="InterfaceEndpointProfileName">The name of the Interface Endpoint Profile to remove</param>
        public void RemoveInterfaceEndpointProfile(string resourceGroupName, string serverName, string InterfaceEndpointProfileName)
        {
            Communicator.Remove(resourceGroupName, serverName, InterfaceEndpointProfileName);
        }

        /// <summary>
        /// Convert a Management.Sql.Models.InterfaceEndpointProfile to AzureSqlServerInterfaceEndpointProfileModel
        /// </summary>
        /// <param name="resourceGroup">The resource group the server is in</param>
        /// <param name="serverName">The name of the server</param>
        /// <param name="resp">The management client server response to convert</param>
        /// <returns>The converted Interface Endpoint Profile model</returns>
        private static AzureSqlServerInterfaceEndpointProfileModel CreateInterfaceEndpointProfileModelFromResponse(string resourceGroup, string serverName, Management.Sql.Models.InterfaceEndpointProfile resp)
        {
            AzureSqlServerInterfaceEndpointProfileModel InterfaceEndpointProfileName = new AzureSqlServerInterfaceEndpointProfileModel();

            InterfaceEndpointProfileName.ResourceGroupName = resourceGroup;
            InterfaceEndpointProfileName.ServerName = serverName;
            InterfaceEndpointProfileName.InterfaceEndpointProfileName = resp.Name;
            InterfaceEndpointProfileName.VirtualNetworkSubnetId = resp.VirtualNetworkSubnetId;
            InterfaceEndpointProfileName.State = resp.State;

            return InterfaceEndpointProfileName;
        }
    }
}
