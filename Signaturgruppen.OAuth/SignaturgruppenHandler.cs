using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Signaturgruppen.OAuth
{
    /// <inheritdoc />
    public class SignaturgruppenHandler : OAuthHandler<SignaturgruppenOptions>
    {
        /// <inheritdoc />
        public SignaturgruppenHandler(IOptionsMonitor<SignaturgruppenOptions> options, ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock) : base(options, logger, encoder, clock)
        {
        }

        /// <inheritdoc />
        protected override string BuildChallengeUrl(AuthenticationProperties properties, string redirectUri)
        {
            var url = base.BuildChallengeUrl(properties, redirectUri);
            var queryString = new Dictionary<string, string> { { "nonce", Guid.NewGuid().ToString("N") } };

            this.Options.Options.BuildQueryString(queryString);

            var finalUrl = QueryHelpers.AddQueryString(url, queryString);
            return finalUrl;
        }

        /// <inheritdoc />
        protected override async Task<AuthenticationTicket> CreateTicketAsync(ClaimsIdentity identity, AuthenticationProperties properties, OAuthTokenResponse tokens)
        {
            var user = await ExtractUser(tokens);
            var context = new OAuthCreatingTicketContext(new ClaimsPrincipal(identity), properties, Context, Scheme, Options, Backchannel, tokens, user);
            context.RunClaimActions();
            await Events.CreatingTicket(context);
            return new AuthenticationTicket(context.Principal, context.Properties, Scheme.Name);
        }

        /// <inheritdoc />
        protected override string FormatScope(IEnumerable<string> scopes)
        {
            return string.Join(" ", scopes);
        }

        private async Task<JsonElement> ExtractUser(OAuthTokenResponse tokens)
        {
            var jwtHandler = new JwtSecurityTokenHandler();
            var jwtInput = tokens.Response.RootElement.GetString("id_token");
            var readableToken = jwtHandler.CanReadToken(jwtInput);

            if (!readableToken) return JsonDocument.Parse("{}").RootElement;

            var token = jwtHandler.ReadJwtToken(jwtInput);
            var claims = token.Claims.ToList();

            var requestMessage = new HttpRequestMessage(HttpMethod.Get, Options.UserInformationEndpoint);
            requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", tokens.AccessToken);
            var responseMessage = await Backchannel.SendAsync(requestMessage);
            responseMessage.EnsureSuccessStatusCode();
            var json = await responseMessage.Content.ReadAsStringAsync();

            var userInfoClaims = JsonDocument.Parse(json).RootElement.EnumerateObject().Select(x => new KeyValuePair<string, string>(x.Name, x.Value.GetString()));

            var jwtPayload = $"{{{string.Join(",", claims.Select(c => $"\"{c.Type}\":\"{c.Value}\"").Union(userInfoClaims.Select(x => $"\"{x.Key}\":\"{x.Value}\"")))}}}";
            return JsonDocument.Parse(jwtPayload).RootElement;

        }
    }
}
