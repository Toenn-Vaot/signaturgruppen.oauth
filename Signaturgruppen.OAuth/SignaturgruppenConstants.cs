namespace Signaturgruppen.OAuth
{
    /// <summary>
    /// The Signaturgruppen claims
    /// </summary>
    public static class SignaturgruppenClaims
    {
        /// <summary>
        /// The basis of all Signaturgruppen claims
        /// </summary>
        public const string BaseClaim = "urn:signaturgruppen:";
    }

    /// <summary>
    /// The available Signaturgruppen locale constants
    /// </summary>
    public static class SignaturgruppenLocales
    {
        /// <summary>
        /// The Greenlandic locale
        /// </summary>
        public const string Greenlandic = "kl";

        /// <summary>
        /// The English locale
        /// </summary>
        public const string English = "en";

        /// <summary>
        /// The Danish locale
        /// </summary>
        public const string Danish = "da";
    }

    /// <summary>
    /// The Signaturgruppen AMR constants
    /// </summary>
    public static class SignaturgruppenAmrValues
    {
        public const string MitidOtp = "password";
        public const string MitidKeyFile = "code_token";
        public const string MitidCodeReader = "code_reader";
        public const string MitidCodeApp = "code_app";
        public const string MitidCodeAppEnchanced = "code_app_enchanced";
        public const string MitidU2FToken = "u2f_token";

        public const string NemIdOtp = "nemid.otp";
        public const string NemIdKeyFile = "nemid.keyfile";
    }

    /// <summary>
    /// The Signaturgruppen response type constants
    /// </summary>
    public static class SignaturgruppenResponseTypes
    {
        /// <summary>
        /// Code
        /// </summary>
        public const string Code = "code";
    }

    /// <summary>
    /// The Signaturgruppen grant type constants
    /// </summary>
    public static class SignaturgruppenGrantTypes
    {
        /// <summary>
        /// The authorization code grant type
        /// </summary>
        public const string AuthorizationCode = "authorization_code";
    }

    /// <summary>
    /// The Signaturgruppen scope constants
    /// </summary>
    public static class SignaturgruppenScopes
    {
        /// <summary>
        /// OpenId
        /// </summary>
        public const string OpenId = "openid";

        /// <summary>
        /// Mitid
        /// </summary>
        public const string Mitid = "mitid";

        /// <summary>
        /// Ssn
        /// </summary>
        public const string Ssn = "ssn";

        /// <summary>
        /// NemID
        /// </summary>
        public const string Nemid = "nemid";
    }

    /// <summary>
    /// The Signaturgruppen subject type constants
    /// </summary>
    public static class EIdentSubjectTypes
    {
        /// <summary>
        /// Public
        /// </summary>
        public const string Public = "public";

        /// <summary>
        /// Pairwise
        /// </summary>
        public const string Pairwise = "pairwise";
    }

    /// <summary>
    /// The Signaturgruppen claim type constants
    /// </summary>
    public static class EIdentClaimTypes
    {
        /// <summary>
        /// Normal
        /// </summary>
        public const string Normal = "normal";

        /// <summary>
        /// Aggregated
        /// </summary>
        public const string Aggregated = "aggregated";

        /// <summary>
        /// Distributed
        /// </summary>
        public const string Distributed = "distributed";
    }

    /// <summary>
    /// The Signaturgruppen authentication method constants
    /// </summary>
    public static class EIdentAuthMethods
    {
        /// <summary>
        /// Client Secret BASIC
        /// </summary>
        public const string ClientSecretBasic = "client_secret_basic";

        /// <summary>
        /// Client Secret POST
        /// </summary>
        public const string ClientSecretPost = "client_secret_post";
    }

    /// <summary>
    /// The Signaturgruppen preset ID constants
    /// </summary>
    public static class EIdentPresetIds
    {
        /// <summary>
        /// SSN
        /// </summary>
        public const string Ssn = "SSN";

        /// <summary>
        /// Phone Number
        /// </summary>
        public const string PhoneNumber = "Phone number";

        /// <summary>
        /// Email Address
        /// </summary>
        public const string EmailAddress = "E-mail address";

        /// <summary>
        /// Name
        /// </summary>
        public const string Name = "name";
    }
}
