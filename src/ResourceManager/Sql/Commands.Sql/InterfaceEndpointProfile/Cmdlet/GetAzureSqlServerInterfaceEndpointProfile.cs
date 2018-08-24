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

using Microsoft.Azure.Commands.Sql.InterfaceEndpointProfile.Model;
using System.Collections.Generic;
using System.Management.Automation;

namespace Microsoft.Azure.Commands.Sql.InterfaceEndpointProfile.Cmdlet
{
    /// <summary>
    /// Defines the Get-AzureRmSqlServerInterfaceEndpointProfile cmdlet
    /// </summary>
    [Cmdlet("Get", ResourceManager.Common.AzureRMConstants.AzureRMPrefix + "SqlServerInterfaceEndpointProfile"),OutputType(typeof(Model.AzureSqlServerInterfaceEndpointProfileModel))]
    public class GetAzureSqlServerInterfaceEndpointProfile : AzureSqlServerInterfaceEndpointProfileCmdletBase
    {
        /// <summary>
        /// Gets or sets the name of the Azure Sql Server Interface Endpoint Profile
        /// </summary>
        [Parameter(Mandatory = false,
            HelpMessage = "The Azure Sql Server Interface Endpoint Profile name.")]
        [ValidateNotNullOrEmpty]
        public string InterfaceEndpointProfileName { get; set; }

        /// <summary>
        /// Gets a Interface Endpoint Profile from the service.
        /// </summary>
        /// <returns>A single Interface Endpoint Profile</returns>
        protected override IEnumerable<AzureSqlServerInterfaceEndpointProfileModel> GetEntity()
        {
            ICollection<AzureSqlServerInterfaceEndpointProfileModel> results = null;

            if (this.MyInvocation.BoundParameters.ContainsKey("InterfaceEndpointProfileName"))
            {
                results = new List<AzureSqlServerInterfaceEndpointProfileModel>();
                results.Add(ModelAdapter.GetInterfaceEndpointProfile(this.ResourceGroupName, this.ServerName, this.InterfaceEndpointProfileName));
            }
            else
            {
                results = ModelAdapter.ListInterfaceEndpointProfiles(this.ResourceGroupName, this.ServerName);
            }

            return results;
        }

        /// <summary>
        /// No changes, thus nothing to persist.
        /// </summary>
        /// <param name="entity">The entity retrieved</param>
        /// <returns>The unchanged entity</returns>
        protected override IEnumerable<AzureSqlServerInterfaceEndpointProfileModel> PersistChanges(IEnumerable<AzureSqlServerInterfaceEndpointProfileModel> entity)
        {
            return entity;
        }

        /// <summary>
        /// No user input to apply to model.
        /// </summary>
        /// <param name="model">The model to modify</param>
        /// <returns>The input model</returns>
        protected override IEnumerable<AzureSqlServerInterfaceEndpointProfileModel> ApplyUserInputToModel(IEnumerable<AzureSqlServerInterfaceEndpointProfileModel> model)
        {
            return model;
        }
    }
}
