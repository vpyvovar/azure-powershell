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

using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;

namespace Microsoft.Azure.Commands.Sql.InterfaceEndpointProfile.Cmdlet
{
    /// <summary>
    /// Defines the Set-AzureRmSqlServerInterfaceEndpointProfile cmdlet
    /// </summary>
    [Cmdlet("Set", ResourceManager.Common.AzureRMConstants.AzureRMPrefix + "SqlServerInterfaceEndpointProfile", SupportsShouldProcess = true, ConfirmImpact = ConfirmImpact.Medium),OutputType(typeof(Model.AzureSqlServerInterfaceEndpointProfileModel))]
    public class SetAzureSqlServerInterfaceEndpointProfile : AzureSqlServerInterfaceEndpointProfileCmdletBase
    {
        /// <summary>
        /// Gets or sets the name of the Azure Sql Server Interface Endpoint Profile
        /// </summary>
        [Parameter(Mandatory = true,
            HelpMessage = "The name of the Azure Sql Server Interface Endpoint Profile.")]
        [ValidateNotNullOrEmpty]
        public string InterfaceEndpointProfileName { get; set; }

        /// <summary>
        /// The Virtual Network Subnet Id
        /// </summary>
        [Parameter(Mandatory = true,
            HelpMessage = "The Virtual Network Subnet Id for the rule.")]
        [ValidateNotNull]
        public string VirtualNetworkSubnetId { get; set; }

        /// <summary>
        /// Gets or sets whether or not to run this cmdlet in the background as a job
        /// </summary>
        [Parameter(Mandatory = false, HelpMessage = "Run cmdlet in the background")]
        public SwitchParameter AsJob { get; set; }

        /// <summary>
        /// Get the Interface Endpoint Profile to update
        /// </summary>
        /// <returns>The Interface Endpoint Profile being updated</returns>
        protected override IEnumerable<Model.AzureSqlServerInterfaceEndpointProfileModel> GetEntity()
        {
            return new List<Model.AzureSqlServerInterfaceEndpointProfileModel>() { ModelAdapter.GetInterfaceEndpointProfile(this.ResourceGroupName, this.ServerName, this.InterfaceEndpointProfileName) };
        }

        /// <summary>
        /// Constructs the model to send to the update API
        /// </summary>
        /// <param name="model">The result of the get operation</param>
        /// <returns>The model to send to the update</returns>
        protected override IEnumerable<Model.AzureSqlServerInterfaceEndpointProfileModel> ApplyUserInputToModel(IEnumerable<Model.AzureSqlServerInterfaceEndpointProfileModel> model)
        {
            // Construct a new entity so we only send the relevant data to the server
            List<Model.AzureSqlServerInterfaceEndpointProfileModel> updateData = new List<Model.AzureSqlServerInterfaceEndpointProfileModel>();
            updateData.Add(new Model.AzureSqlServerInterfaceEndpointProfileModel()
            {
                ResourceGroupName = this.ResourceGroupName,
                ServerName = this.ServerName,
                InterfaceEndpointProfileName = this.InterfaceEndpointProfileName,
                VirtualNetworkSubnetId = this.VirtualNetworkSubnetId
            });
            return updateData;
        }

        /// <summary>
        /// Sends the VirtualNetwork Rule update request to the service
        /// </summary>
        /// <param name="entity">The update parameters</param>
        /// <returns>The response object from the service</returns>
        protected override IEnumerable<Model.AzureSqlServerInterfaceEndpointProfileModel> PersistChanges(IEnumerable<Model.AzureSqlServerInterfaceEndpointProfileModel> entity)
        {
            return new List<Model.AzureSqlServerInterfaceEndpointProfileModel>() {
                ModelAdapter.UpsertInterfaceEndpointProfile(entity.First())
            };
        }
    }
}
