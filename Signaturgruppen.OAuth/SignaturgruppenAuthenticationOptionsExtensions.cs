using System;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.DependencyInjection;

namespace Signaturgruppen.OAuth
{
    /// <summary>
    /// The Signaturgruppen authentication options extensions
    /// </summary>
    public static class SignaturgruppenAuthenticationOptionsExtensions
    {
        /// <summary>
        /// Add Signaturgruppen to authentication builder
        /// </summary>
        /// <param name="builder">The <see cref="AuthenticationBuilder"/></param>
        /// <returns>The authentication builder with EIdent</returns>
        public static AuthenticationBuilder AddSignaturgruppen(this AuthenticationBuilder builder)
        {
            return builder.AddSignaturgruppen(SignaturgruppenDefaults.AuthenticationScheme, _ => { });
        }

        /// <summary>
        /// Add Signaturgruppen to authentication builder with options
        /// </summary>
        /// <param name="builder">The <see cref="AuthenticationBuilder"/></param>
        /// <param name="configureOptions">The configuration options</param>
        /// <returns>The authentication builder with EIdent</returns>
        public static AuthenticationBuilder AddSignaturgruppen(this AuthenticationBuilder builder, Action<SignaturgruppenOptions> configureOptions)
        {
            return builder.AddSignaturgruppen(SignaturgruppenDefaults.AuthenticationScheme, configureOptions);
        }

        /// <summary>
        /// Add Signaturgruppen to authentication builder with defined authentication scheme and options
        /// </summary>
        /// <param name="builder">The <see cref="AuthenticationBuilder"/></param>
        /// <param name="authenticationScheme">The authentication scheme</param>
        /// <param name="configureOptions">The configuration options</param>
        /// <returns>The authentication builder with EIdent</returns>
        public static AuthenticationBuilder AddSignaturgruppen(this AuthenticationBuilder builder, string authenticationScheme, Action<SignaturgruppenOptions> configureOptions)
        {
            return builder.AddSignaturgruppen(authenticationScheme, SignaturgruppenDefaults.DisplayName, configureOptions);
        }

        /// <summary>
        /// Add Signaturgruppen to authentication builder with defined authentication scheme, display name and options
        /// </summary>
        /// <param name="builder">The <see cref="AuthenticationBuilder"/></param>
        /// <param name="authenticationScheme">The authentication scheme</param>
        /// <param name="displayName">The display name</param>
        /// <param name="configureOptions">The configuration options</param>
        /// <returns>The authentication builder with EIdent</returns>
        public static AuthenticationBuilder AddSignaturgruppen(this AuthenticationBuilder builder, string authenticationScheme, string displayName, Action<SignaturgruppenOptions> configureOptions)
        {
            return builder.AddOAuth<SignaturgruppenOptions, SignaturgruppenHandler>(authenticationScheme, displayName, configureOptions);
        }
    }
}
