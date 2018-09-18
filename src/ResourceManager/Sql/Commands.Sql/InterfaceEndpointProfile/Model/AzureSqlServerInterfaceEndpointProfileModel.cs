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

namespace Microsoft.Azure.Commands.Sql.InterfaceEndpointProfile.Model
{
    /// <summary>
    /// Represents the core properties of an Azure Sql Server Interface Endpoint Profile
    /// </summary>
    public class AzureSqlServerInterfaceEndpointProfileModel
    {
        /// <summary>
        /// Gets or sets the name of the resource group
        /// </summary>
        public string ResourceGroupName { get; set; }

        /// <summary>
        /// Gets or sets the name of the server
        /// </summary>
        public string ServerName { get; set; }

        /// <summary>
        /// Gets or sets the name of the Interface Endpoint Profile
        /// </summary>
        public string InterfaceEndpointProfileName { get; set; }

        /// <summary>
        /// Gets or sets the url of the virtual network
        /// </summary>
        public string VirtualNetworkSubnetId { get; set; }

        /// <summary>
        /// Gets or sets the private ip
        /// </summary>
        public string PrivateIp { get; set; }

        /// <summary>
        /// Gets Interface Endpoint Profile state. Possible values include:
        /// 'Initializing', 'InProgress', 'Ready', 'Deleting', 'Unknown'
        /// </summary>
        public string State { get; set; }
    }
}
