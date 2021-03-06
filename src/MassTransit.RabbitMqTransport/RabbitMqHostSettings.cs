﻿// Copyright 2007-2016 Chris Patterson, Dru Sellers, Travis Smith, et. al.
//  
// Licensed under the Apache License, Version 2.0 (the "License"); you may not use
// this file except in compliance with the License. You may obtain a copy of the 
// License at 
// 
//     http://www.apache.org/licenses/LICENSE-2.0 
// 
// Unless required by applicable law or agreed to in writing, software distributed
// under the License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR 
// CONDITIONS OF ANY KIND, either express or implied. See the License for the 
// specific language governing permissions and limitations under the License.
namespace MassTransit.RabbitMqTransport
{
    using System;
    using System.Net.Security;
    using System.Security.Authentication;
    using System.Security.Cryptography.X509Certificates;
    using Transports;


    /// <summary>
    /// Settings to configure a RabbitMQ host explicitly without requiring the fluent interface
    /// </summary>
    public interface RabbitMqHostSettings
    {
        /// <summary>
        ///     The RabbitMQ host to connect to (should be a valid hostname)
        /// </summary>
        string Host { get; }

        /// <summary>
        ///     The RabbitMQ port to connect
        /// </summary>
        int Port { get; }

        /// <summary>
        ///     The virtual host for the connection
        /// </summary>
        string VirtualHost { get; }

        /// <summary>
        ///     The Username for connecting to the host
        /// </summary>
        string Username { get; }

        /// <summary>
        ///     The password for connection to the host
        ///     MAYBE this should be a SecureString instead of a regular string
        /// </summary>
        string Password { get; }

        /// <summary>
        ///     The heartbeat interval (in seconds) to keep the host connection alive
        /// </summary>
        ushort Heartbeat { get; }

        /// <summary>
        /// True if SSL is required
        /// </summary>
        bool Ssl { get; }

        /// <summary>
        /// SSL protocol, Tls11 or Tls12 are recommended
        /// </summary>
        SslProtocols SslProtocol { get; }

        /// <summary>
        /// The server name specified on the certificate for the RabbitMQ server
        /// </summary>
        string SslServerName { get; }

        /// <summary>
        /// The acceptable policy errors for the SSL connection
        /// </summary>
        SslPolicyErrors AcceptablePolicyErrors { get; }

        /// <summary>
        /// The path to the client certificate if client certificate authentication is used
        /// </summary>
        string ClientCertificatePath { get; }

        /// <summary>
        /// The passphrase for the client certificate found using the <see cref="ClientCertificatePath"/>, not required if <see cref="ClientCertificate"/> is populated
        /// </summary>
        string ClientCertificatePassphrase { get; }

        /// <summary>
        /// A certificate to use for client certificate authentication, if not set then the <see cref="ClientCertificatePath"/> and <see cref="ClientCertificatePassphrase"/> will be used
        /// </summary>
        X509Certificate ClientCertificate { get; }

        /// <summary>
        /// Whether the client certificate should be used for logging in to RabbitMQ, ignoring any username and password set
        /// </summary>
        /// <remarks>
        /// RabbitMQ must be configured correctly for this to work, including enabling the rabbitmq_auth_mechanism_ssl plugin
        /// </remarks>
        bool UseClientCertificateAsAuthenticationIdentity { get; }

        /// <summary>
        /// The message name formatter for the publisher
        /// </summary>
        IMessageNameFormatter MessageNameFormatter { get; }

        /// <summary>
        /// When using a RabbitMQ cluster, this contains the host names which make up the cluster. In the event of a connection failure, the next host in the array will be connected to.
        /// </summary>
        string[] ClusterMembers { get; }

        /// <summary>
        /// The host name selector if used to choose which server to connect
        /// </summary>
        IRabbitMqEndpointResolver HostNameSelector { get; }

        /// <summary>
        /// The client-provided name for the connection (displayed in RabbitMQ admin panel)
        /// </summary>
        string ClientProvidedName { get; }

        /// <summary>
        /// Returns the host address
        /// </summary>
        Uri HostAddress { get; }

        /// <summary>
        /// True if the publisher should confirm acceptance of messages
        /// </summary>
        bool PublisherConfirmation { get; }
    }
}