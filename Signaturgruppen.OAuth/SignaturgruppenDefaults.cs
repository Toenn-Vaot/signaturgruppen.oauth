namespace Signaturgruppen.OAuth
{
    /// <summary>
    /// The Signaturgruppen default value constants
    /// </summary>
    public class SignaturgruppenDefaults
    {
        /// <summary>
        /// The display name
        /// </summary>
        public static readonly string DisplayName = "Signaturgruppen";

        /// <summary>
        /// The authorization endpoint for test
        /// </summary>
        public static readonly string AuthorizationEndpointTest = "https://pp.netseidbroker.dk/op/connect/authorize";

        /// <summary>
        /// The token endpoint for test
        /// </summary>
        public static readonly string TokenEndpointTest = "https://pp.netseidbroker.dk/op/connect/token";

        /// <summary>
        /// The User-Information endpoint for test
        /// </summary>
        public static readonly string UserInformationEndpointTest = "https://pp.netseidbroker.dk/op/connect/userinfo";
        
        /// <summary>
        /// The authorization endpoint
        /// </summary>
        public static readonly string AuthorizationEndpoint = "https://netseidbroker.mitid.dk/op/connect/authorize";

        /// <summary>
        /// The token endpoint
        /// </summary>
        public static readonly string TokenEndpoint = "https://netseidbroker.mitid.dk/op/connect/token";

        /// <summary>
        /// The User-Information endpoint
        /// </summary>
        public static readonly string UserInformationEndpoint = "https://netseidbroker.mitid.dk/op/connect/userinfo";

        /// <summary>
        /// The authentication scheme
        /// </summary>
        public const string AuthenticationScheme = "Signaturgruppen";
    }
}
