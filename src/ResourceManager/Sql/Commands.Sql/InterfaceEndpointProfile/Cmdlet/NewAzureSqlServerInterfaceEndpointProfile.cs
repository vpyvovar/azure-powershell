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

using Microsoft.Rest.Azure;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;

namespace Microsoft.Azure.Commands.Sql.InterfaceEndpointProfile.Cmdlet
{
    /// <summary>
    /// Defines the New-AzureRmSqlServerInterfaceEndpointProfile cmdlet
    /// </summary>
    [Cmdlet("New", ResourceManager.Common.AzureRMConstants.AzureRMPrefix + "SqlServerInterfaceEndpointProfile", SupportsShouldProcess = true),OutputType(typeof(Model.AzureSqlServerInterfaceEndpointProfileModel))]
    public class NewAzureSqlServerInterfaceEndpointProfile : AzureSqlServerInterfaceEndpointProfileCmdletBase
    {
        /// <summary>
        /// Azure Sql Server Interface Endpoint Profile Name.
        /// </summary>
        [Parameter(Mandatory = true,
            HelpMessage = "Azure Sql Server Interface Endpoint Profile Name.")]
        [ValidateNotNullOrEmpty]
        public string InterfaceEndpointProfileName { get; set; }

        /// <summary>
        /// The Virtual Network Subnet Id
        /// </summary>
        [Parameter(Mandatory = true,
            HelpMessage = "The Virtual Network Subnet Id that specifies the Microsoft.Network details")]
        [ValidateNotNull]
        public string VirtualNetworkSubnetId { get; set; }

        /// <summary>
        /// Gets or sets whether or not to run this cmdlet in the background as a job
        /// </summary>
        [Parameter(Mandatory = false, HelpMessage = "Run cmdlet in the background")]
        public SwitchParameter AsJob { get; set; }

        /// <summary>
        /// Check to see if the Interface Endpoint Profile already exists for this server
        /// </summary>
        /// <returns>Null if the Interface Endpoint Profile doesn't exist.  Otherwise throws exception</returns>
        protected override IEnumerable<Model.AzureSqlServerInterfaceEndpointProfileModel> GetEntity()
        {
            try
            {
                ModelAdapter.GetInterfaceEndpointProfile(this.ResourceGroupName, this.ServerName, this.InterfaceEndpointProfileName);
            }
            catch (CloudException ex)
            {
                if (ex.Response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    // This is what we want.  We looked and there is no Interface Endpoint Profile with this name.
                    return null;
                }

                // Unexpected exception encountered
                throw;
            }

            // The server already exists
            throw new PSArgumentException(
                string.Format(Microsoft.Azure.Commands.Sql.Properties.Resources.ServerInterfaceEndpointProfileNameExists, this.InterfaceEndpointProfileName, this.ServerName),
                "InterfaceEndpointProfile");
        }

        /// <summary>
        /// Generates the model from user input.
        /// </summary>
        /// <param name="model">This is null since the Interface Endpoint Profile doesn't exist yet</param>
        /// <returns>The generated model from user input</returns>
        protected override IEnumerable<Model.AzureSqlServerInterfaceEndpointProfileModel> ApplyUserInputToModel(IEnumerable<Model.AzureSqlServerInterfaceEndpointProfileModel> model)
        {
            List<Model.AzureSqlServerInterfaceEndpointProfileModel> newEntity = new List<Model.AzureSqlServerInterfaceEndpointProfileModel>();
            newEntity.Add(new Model.AzureSqlServerInterfaceEndpointProfileModel()
            {
                ResourceGroupName = this.ResourceGroupName.Trim(),
                ServerName = this.ServerName.Trim(),
                InterfaceEndpointProfileName = this.InterfaceEndpointProfileName.Trim(),
                VirtualNetworkSubnetId = this.VirtualNetworkSubnetId.Trim()
            });
            return newEntity;
        }

        /// <summary>
        /// Sends the changes to the service -> Creates the Interface Endpoint Profile
        /// </summary>
        /// <param name="entity">The server to create</param>
        /// <returns>The created server</returns>
        protected override IEnumerable<Model.AzureSqlServerInterfaceEndpointProfileModel> PersistChanges(IEnumerable<Model.AzureSqlServerInterfaceEndpointProfileModel> entity)
        {
            return new List<Model.AzureSqlServerInterfaceEndpointProfileModel>() {
                ModelAdapter.UpsertInterfaceEndpointProfile(entity.First())
            };
        }
    }
}
