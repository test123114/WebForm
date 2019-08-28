using Microsoft.IdentityModel.Protocols;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Web;

namespace TestWebForm
{
    public class GenUtil
    {
        public static string token;
        public static string user;

        public static JwtSecurityToken Validate(string token)
        {
            string stsDiscoveryEndpoint = "https://login.microsoftonline.com/hanxia.onmicrosoft.com/v2.0/.well-known/openid-configuration";

            ConfigurationManager<OpenIdConnectConfiguration> configManager = new ConfigurationManager<OpenIdConnectConfiguration>(stsDiscoveryEndpoint, new OpenIdConnectConfigurationRetriever());

            OpenIdConnectConfiguration config = configManager.GetConfigurationAsync().Result;

            TokenValidationParameters validationParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                IssuerSigningKeys = config.SigningKeys,
                ValidateLifetime = false
            };
            var keys = config.SigningKeys;
            JwtSecurityTokenHandler tokendHandler = new JwtSecurityTokenHandler();

            SecurityToken jwt;

            var result = tokendHandler.ValidateToken(token, validationParameters, out jwt);

            return jwt as JwtSecurityToken;
        }

    }

   
}