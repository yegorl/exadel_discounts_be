﻿using Exadel.CrazyPrice.IdentityServer.Configuration;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using Xunit;

namespace Exadel.CrazyPrice.Tests.IdentityServer.Configuration
{
    public class IdentityServerConfigurationTests
    {
        [Fact]
        public void IdentityServerConfigurationTest()
        {
            var config = new ConfigurationBuilder()
                .AddInMemoryCollection(new KeyValuePair<string, string>[]
                {
                    new("Auth:Origins:0", "\"https://localhost\""),
                    new("Auth:PolicyName","PolicyName"),
                    new("Auth:IssuerUrl","IssuerUrl"),
                    new("Certificate:Name","CertificateName"),
                    new("Certificate:Password","CertificatePassword"),
                    new("ApiResources:0:ApiSecrets:0:Description",""),
                    new("ApiScopes:0:Description",""),
                    new("Clients:0:Description","0"),
                    new("IdentityResources:0:Description",""),
                })
                .Build();

            var isConfig = new IdentityServerConfiguration(config);

            var origins = isConfig.Origins;
            origins.Should().NotBeNull();

            var policyName = isConfig.PolicyName;
            policyName.Should().NotBeNull();

            var issuerUrl = isConfig.IssuerUrl;
            issuerUrl.Should().NotBeNull();

            var certificateName = isConfig.CertificateName;
            certificateName.Should().NotBeNull();

            var certificatePassword = isConfig.CertificatePassword;
            certificatePassword.Should().NotBeNull();

            var clients = isConfig.Clients;
            clients.Should().NotBeNull();

            var apiScopes = isConfig.ApiScopes;
            apiScopes.Should().NotBeNull();

            var apiResources = isConfig.ApiResources;
            apiResources.Should().NotBeNull();

            var identityResources = isConfig.IdentityResources;
            identityResources.Should().NotBeNull();
        }
    }
}
