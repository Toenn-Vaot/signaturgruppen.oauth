using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OAuth;

namespace Signaturgruppen.OAuth
{
    /// <inheritdoc />
    public class SignaturgruppenOptions : OAuthOptions
    {
        /// <summary>
        /// The Signaturgruppen request options
        /// </summary>
        public SignaturgruppenRequestOptions Options { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <remarks>
        /// <p>References: https://www.nets.eu/developer/e-ident/overview/Pages/oidc.aspx</p>
        /// <p>Discovery URL : https://netseidbroker.dk/op/.well-known/openid-configuration</p>
        /// </remarks>
        public SignaturgruppenOptions()
        {
            AuthorizationEndpoint = SignaturgruppenDefaults.AuthorizationEndpoint;
            TokenEndpoint = SignaturgruppenDefaults.TokenEndpoint;
            UserInformationEndpoint = SignaturgruppenDefaults.UserInformationEndpoint;
            CallbackPath = "/signin-signaturgruppen";
            ClaimsIssuer = "https://netsbroker.mitid.dk/op";
            
            Scope.Add(SignaturgruppenScopes.OpenId);
            ClaimActions.MapJsonKey($"{SignaturgruppenClaims.BaseClaim}acr", "acr");
            ClaimActions.MapJsonKey($"{SignaturgruppenClaims.BaseClaim}amr", "amr");
            ClaimActions.MapJsonKey($"{SignaturgruppenClaims.BaseClaim}aud", "aud"); //The unique customer "MID" value
            ClaimActions.MapJsonKey(ClaimTypes.Authentication, "auth_time"); //​Time when the end user identification occurred. - The value is a JSON number representing the number of seconds from 1970-01-01T0:0:0Z as measured in UTC until the date/time.

            ClaimActions.MapJsonKey($"{SignaturgruppenClaims.BaseClaim}exp", "exp"); //Expiration time on or after which the ID Token must not be accepted for processing. - The value is a JSON number representing the number of seconds from 1970-01-01T0:0:0Z as measured in UTC until the date/time.

            ClaimActions.MapJsonKey($"{SignaturgruppenClaims.BaseClaim}iat", "iat"); //Time at which the JWT was issued. - The value is a JSON number representing the number of seconds from 1970-01-01T0:0:0Z as measured in UTC until the date/time.
            ClaimActions.MapJsonKey($"{SignaturgruppenClaims.BaseClaim}iss", "iss"); //Issuer Identifier for the Issuer of the response.

            ClaimActions.MapJsonKey($"{SignaturgruppenClaims.BaseClaim}nonce", "nonce");

            ClaimActions.MapJsonKey($"{SignaturgruppenClaims.BaseClaim}sub", "sub"); //Subject Identifier. - A string in the format "eID type:ID"
        }

        /// <summary>
        /// Configure the Signaturgruppen based on environment
        /// </summary>
        /// <param name="environment">The targeted environment</param>
        public void ConfigureEnvironment(SignaturgruppenEnvironment environment)
        {
            AuthorizationEndpoint = environment == SignaturgruppenEnvironment.Prod ? SignaturgruppenDefaults.AuthorizationEndpoint : SignaturgruppenDefaults.AuthorizationEndpointTest;
            TokenEndpoint = environment == SignaturgruppenEnvironment.Prod ? SignaturgruppenDefaults.TokenEndpoint : SignaturgruppenDefaults.TokenEndpointTest;
            UserInformationEndpoint = environment == SignaturgruppenEnvironment.Prod ? SignaturgruppenDefaults.UserInformationEndpoint : SignaturgruppenDefaults.UserInformationEndpointTest;
        }

        /// <summary>
        /// Add <paramref name="scopes"/> and depending claims 
        /// </summary>
        /// <param name="scopes">The scopes</param>
        public void AddScopesWithClaims(params string[] scopes)
        {
            if (scopes.Contains(SignaturgruppenScopes.Mitid))
            {
                AddMitidLevel();
            }

            if (scopes.Contains(SignaturgruppenScopes.Nemid))
            {
                AddNemidLevel();
            }

            if (scopes.Contains(SignaturgruppenScopes.Ssn))
            {
                AddSsnLevel();
            }
        }

        private void AddMitidLevel()
        {
            Scope.Add(SignaturgruppenScopes.Mitid);
            ClaimActions.MapJsonKey(ClaimTypes.Name, "mitid.identity_name");
            ClaimActions.MapJsonKey(ClaimTypes.DateOfBirth, "mitid.date_of_birth");

            ClaimActions.MapJsonKey($"{SignaturgruppenClaims.BaseClaim}​mitid.uuid", "mitid.uuid");
            ClaimActions.MapJsonKey($"{SignaturgruppenClaims.BaseClaim}​mitid.date_of_birth", "mitid.date_of_birth");
            ClaimActions.MapJsonKey($"{SignaturgruppenClaims.BaseClaim}​mitid.age", "​mitid.age");
            ClaimActions.MapJsonKey($"{SignaturgruppenClaims.BaseClaim}​mitid.identity_name", "mitid.identity_name");
            ClaimActions.MapJsonKey($"{SignaturgruppenClaims.BaseClaim}​mitid.ial_identity_assurance_level", "mitid.ial_identity_assurance_level");
        }

        private void AddNemidLevel()
        {
            Scope.Add(SignaturgruppenScopes.Nemid);
            ClaimActions.MapJsonKey(ClaimTypes.Email, "nemid.email");
            ClaimActions.MapJsonKey(ClaimTypes.Name, "nemid.common_name");

            ClaimActions.MapJsonKey($"{SignaturgruppenClaims.BaseClaim}nemid.common_name", "nemid.common_name");
            ClaimActions.MapJsonKey($"{SignaturgruppenClaims.BaseClaim}nemid.pid", "nemid.pid");
            ClaimActions.MapJsonKey($"{SignaturgruppenClaims.BaseClaim}nemid.rid", "nemid.rid");
            ClaimActions.MapJsonKey($"{SignaturgruppenClaims.BaseClaim}nemid.dn", "nemid.dn");
            ClaimActions.MapJsonKey($"{SignaturgruppenClaims.BaseClaim}nemid.ssn", "nemid.ssn");
            ClaimActions.MapJsonKey($"{SignaturgruppenClaims.BaseClaim}nemid.cvr", "nemid.cvr");
            ClaimActions.MapJsonKey($"{SignaturgruppenClaims.BaseClaim}nemid.auth_to_repr", "nemid.auth_to_repr");
            ClaimActions.MapJsonKey($"{SignaturgruppenClaims.BaseClaim}nemid.email", "nemid.email");
        }

        private void AddSsnLevel()
        {
            Scope.Add(SignaturgruppenScopes.Ssn);
            ClaimActions.MapJsonKey($"{SignaturgruppenClaims.BaseClaim}dk.cpr", "dk.cpr");
            ClaimActions.MapJsonKey(ClaimTypes.NameIdentifier, "dk.cpr");
        }
    }

    /// <summary>
    /// The EIdent environment available enumeration list
    /// </summary>
    public enum SignaturgruppenEnvironment
    {
        /// <summary>
        /// Production
        /// </summary>
        Prod = 0,
        /// <summary>
        /// The test environment
        /// </summary>
        /// <remark>Should be used during development or in test environment</remark>
        Test = 1
    }

    /// <summary>
    /// The Signaturgruppen request options
    /// </summary>
    public class SignaturgruppenRequestOptions
    {
        /// <summary>
        /// The environment to execute the Signaturgruppen authentication
        /// </summary>
        public SignaturgruppenEnvironment Environment { get; set; } = SignaturgruppenEnvironment.Prod;

        /// <summary>
        /// ​Maximum Authentication Age. Specifies the allowable elapsed time in seconds since the last time the end user was actively authenticated.
        /// If the elapsed time is greater than this value, then  the customer should attempt to actively re-authenticate the end user. When <see cref="MaxAge"/> is used,
        /// the ID Token returned should include an auth_time Claim Value.
        /// </summary>
        /// <example>600 for 10 minutes</example>
        public int MaxAge { get; set; }

        /// <summary>
        /// Requested Authentication Method Reference values. Space-separated string that specifies the "amr" values that the Authorization Server is being requested to use
        /// for processing this Authentication Request, with the values appearing in order of preference.
        /// The authentication methods used for the authentication performed are returned as the amr Claim Value.
        /// This parameter is equivalent to the <strong>forcepkivendor</strong> parameter of SAML identification.
        /// </summary>
        /// <seealso cref="SignaturgruppenAmrValues"/>
        public string[] Amr { get; set; }

        /// <summary>
        /// The language used to provide user with information during identification. If not provided, then E-Ident uses the language specified by the web browser.
        /// </summary>
        /// <remarks>If no supported languages are available in the browser, or the parameter, then Norwegian is used by default.</remarks>
        /// <example>nb_NO, en_GB, da_DK, sv_SE, fi_FI, sv_FI</example>
        /// <seealso cref="SignaturgruppenLocales"/>
        public string Locale { get; set; }

        /// <summary>
        /// ​​Transaction text displayed in the end user's NemID code app.
        /// </summary>
        /// <remarks>Only usable for <see cref="SignaturgruppenAmrValues.NemIdOtp"/></remarks>
        public string TransactionText { get; set; }

        /// <summary>
        /// Method to build the query string with request option values
        /// </summary>
        /// <param name="queryString">The query string dictionary to complete</param>
        public void BuildQueryString(Dictionary<string, string> queryString)
        {
            queryString.Add("prompt", "login");

            if (Amr != null && Amr.Length > 0)
                queryString.Add("amr_values", string.Join(" ", Amr));

            if (MaxAge != default && MaxAge > 0)
                queryString.Add("max_age", MaxAge.ToString());

            if (!string.IsNullOrWhiteSpace(Locale))
                queryString.Add("language", Locale);

            if (!string.IsNullOrWhiteSpace(TransactionText))
                queryString.Add("transactiontext", TransactionText);

        }
    }
}
