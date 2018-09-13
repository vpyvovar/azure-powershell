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
using System.Globalization;
using System.Management.Automation;
using System.Linq;

namespace Microsoft.Azure.Commands.Sql.InterfaceEndpointProfile.Cmdlet
{
    /// <summary>
    /// Defines the Remove-AzureRmSqlServerInterfaceEndpointProfile cmdlet
    /// </summary>
    [Cmdlet("Remove", ResourceManager.Common.AzureRMConstants.AzureRMPrefix + "SqlServerInterfaceEndpointProfile", SupportsShouldProcess = true),OutputType(typeof(Model.AzureSqlServerInterfaceEndpointProfileModel))]
    public class RemoveAzureSqlServerInterfaceEndpointProfile : AzureSqlServerInterfaceEndpointProfileCmdletBase
    {
        /// <summary>
        /// Gets or sets the name of the VirtualNetwork rule to remove
        /// </summary>
        [Parameter(Mandatory = true,
            HelpMessage = "Azure Sql Server Interface Endpoint Profile name")]
        [ValidateNotNullOrEmpty]
        public string InterfaceEndpointProfileName { get; set; }

        /// <summary>
        /// Gets or sets whether or not to run this cmdlet in the background as a job
        /// </summary>
        [Parameter(Mandatory = false, HelpMessage = "Run cmdlet in the background")]
        public SwitchParameter AsJob { get; set; }

        /// <summary>
        /// Gets the entity to delete
        /// </summary>
        /// <returns>The entity going to be deleted</returns>
        protected override IEnumerable<Model.AzureSqlServerInterfaceEndpointProfileModel> GetEntity()
        {
            return new List<Model.AzureSqlServerInterfaceEndpointProfileModel>() {
                ModelAdapter.GetInterfaceEndpointProfile(this.ResourceGroupName, this.ServerName, this.InterfaceEndpointProfileName)
            };
        }

        /// <summary>
        /// Apply user input.  Here nothing to apply
        /// </summary>
        /// <param name="model">The result of GetEntity</param>
        /// <returns>The input model</returns>
        protected override IEnumerable<Model.AzureSqlServerInterfaceEndpointProfileModel> ApplyUserInputToModel(IEnumerable<Model.AzureSqlServerInterfaceEndpointProfileModel> model)
        {
            return model;
        }

        /// <summary>
        /// Deletes the Interface Endpoint Profile.
        /// </summary>
        /// <param name="entity">The Interface Endpoint Profile being deleted</param>
        /// <returns>The input model</returns>
        protected override IEnumerable<Model.AzureSqlServerInterfaceEndpointProfileModel> PersistChanges(IEnumerable<Model.AzureSqlServerInterfaceEndpointProfileModel> entity)
        {
            ModelAdapter.RemoveInterfaceEndpointProfile(this.ResourceGroupName, this.ServerName, this.InterfaceEndpointProfileName);
            entity.First().State = "Deleted";
            return entity;
        }
    }
}
