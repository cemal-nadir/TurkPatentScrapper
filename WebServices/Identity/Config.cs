// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityServer4;
using IdentityServer4.Models;
using System.Collections.Generic;

namespace TPHunter.WebServices.Identity.API
{
    public static class Config
    {
        public static IEnumerable<ApiResource> ApiResources => new[]
        {
            new ApiResource("resource_scrap"){Scopes={ "scrap_full_perm" } },
            new ApiResource(IdentityServerConstants.LocalApi.ScopeName)
        };
        //public static IEnumerable<IdentityResource> IdentityResources =>
        //    new IdentityResource[]
        //    {
        //        new IdentityResources.Email(),
        //        new IdentityResources.OpenId(),
        //        new IdentityResources.Profile(),
        //        new IdentityResources.Phone(),
        //        new IdentityResource(){Name="roles",DisplayName="Roles",Description="Kullanıcı rolleri",UserClaims=new[]{"role" } }
        //    };
        public static IEnumerable<ApiScope> ApiScopes =>
            new[]
            {
          
              new ApiScope("scrap_full_perm","Scrap Api için Full Yetki"),
              new ApiScope(IdentityServerConstants.LocalApi.ScopeName)
            };

        public static IEnumerable<Client> Clients =>
            new[]
            {
               
                   new Client
                {
                    ClientName="Data Supplier Client",
                    ClientId="DataSupplierClient",
                    ClientSecrets={new Secret("cv7VzznOzv8RrqLaaSJA4jZ5GeB3TvM8".Sha256())},
                    AllowedGrantTypes=GrantTypes.ClientCredentials,
                    AllowedScopes={ "scrap_full_perm", IdentityServerConstants.LocalApi.ScopeName }
                }





                   //   new Client
                //{
                //    ClientName="React App Customer Client",
                //    ClientId="AppCustomerClient",
                //    AllowOfflineAccess=true,
                //    ClientSecrets={new Secret("mJiHaBCDcEY8CCDLPYoE3ofW6015T0Kk".Sha256())},
                //    AllowedGrantTypes=GrantTypes.ResourceOwnerPassword,
                //    AllowedScopes={IdentityServerConstants.StandardScopes.Email,IdentityServerConstants.StandardScopes.Phone,IdentityServerConstants.StandardScopes.OpenId,IdentityServerConstants.StandardScopes.Profile,IdentityServerConstants.StandardScopes.OfflineAccess, IdentityServerConstants.LocalApi.ScopeName,"roles" },
                //    AccessTokenLifetime=1*60*60,
                //    RefreshTokenExpiration=TokenExpiration.Absolute,
                //    AbsoluteRefreshTokenLifetime=(int)(DateTime.Now.AddDays(60)-DateTime.Now).TotalSeconds,
                //    RefreshTokenUsage=TokenUsage.ReUse
                //},
                //     new Client
                //{
                //    ClientName="React App Mentor Client",
                //    ClientId="AppMentorClient",
                //    AllowOfflineAccess=true,
                //    ClientSecrets={new Secret("v4LTf5ExpsaABQfHauctPTdG5ceqBedi".Sha256())},
                //    AllowedGrantTypes=GrantTypes.ResourceOwnerPassword,
                //    AllowedScopes={IdentityServerConstants.StandardScopes.Email,IdentityServerConstants.StandardScopes.Phone,IdentityServerConstants.StandardScopes.OpenId,IdentityServerConstants.StandardScopes.Profile,IdentityServerConstants.StandardScopes.OfflineAccess, IdentityServerConstants.LocalApi.ScopeName,"roles" },
                //    AccessTokenLifetime=1*60*60,
                //    RefreshTokenExpiration=TokenExpiration.Absolute,
                //    AbsoluteRefreshTokenLifetime=(int)(DateTime.Now.AddDays(60)-DateTime.Now).TotalSeconds,
                //    RefreshTokenUsage=TokenUsage.ReUse
                //},
                //       new Client
                //{
                //    ClientName="React App Full Client",
                //    ClientId="AppFullClient",
                //    AllowOfflineAccess=true,
                //    ClientSecrets={new Secret("SmNK7ieDbESioB597WrV3ptABnGDYqCK".Sha256())},
                //    AllowedGrantTypes=GrantTypes.ResourceOwnerPassword,
                //    AllowedScopes={IdentityServerConstants.StandardScopes.Email,IdentityServerConstants.StandardScopes.Phone,IdentityServerConstants.StandardScopes.OpenId,IdentityServerConstants.StandardScopes.Profile,IdentityServerConstants.StandardScopes.OfflineAccess, IdentityServerConstants.LocalApi.ScopeName,"roles" },
                //    AccessTokenLifetime=1*60*60,
                //    RefreshTokenExpiration=TokenExpiration.Absolute,
                //    AbsoluteRefreshTokenLifetime=(int)(DateTime.Now.AddDays(60)-DateTime.Now).TotalSeconds,
                //    RefreshTokenUsage=TokenUsage.ReUse
                //}
            };
    }
}