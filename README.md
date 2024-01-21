# Signaturgruppen.Oauth ![example workflow](https://github.com/Toenn-Vaot/signaturgruppen.oauth/actions/workflows/build-release.yml/badge.svg)
Library to help developers implement the authentication on
their application through Signaturgruppen services suing MitID.


```csharp
...

var authenticationBuilder = services.AddAuthentication();
authenticationBuilder.AddSignaturgruppen(SignaturgruppenDefaults.AuthenticationScheme, "signaturgruppen", options =>
{
  options.Options = new SignaturgruppenRequestOptions
  {
    Environment = (Configuration["Signaturgruppen:Environment"] == "Test") ? SignaturgruppenEnvironment.Test : SignaturgruppenEnvironment.Prod,
    Amr = new[] { SignaturgruppenAmrValues.MitidOtp, SignaturgruppenAmrValues.NemIdOtp },
    Locale = SignaturgruppenLocales.English
  };

  options.Events.OnRemoteFailure = ctx =>
  {
    var queryParams = ctx.Request.QueryString.ToString();
    if (ctx.Request.Method == "POST")
    {
      queryParams = QueryHelpers.AddQueryString("", ctx.Request.Form?.ToDictionary(p => p.Key, p => p.Value.ToString()));
    }

    ctx.Response.Redirect($"/Account/RemoteFailure{queryParams}");
    ctx.HandleResponse();
    return Task.FromResult(0);
  };

  options.AccessDeniedPath = CookieAuthenticationDefaults.AccessDeniedPath;
  options.ConfigureEnvironment(Configuration["Signaturgruppen:Environment"] == "Test" ? SignaturgruppenEnvironment.Test : SignaturgruppenEnvironment.Prod);
  options.SignInScheme = IdentityConstants.ExternalScheme;
  options.ClientId = Configuration["Signaturgruppen:Key"];
  options.ClientSecret = Configuration["Signaturgruppen:Secret"];
  options.AddScopesWithClaims(SignaturgruppenScopes.Ssn, SignaturgruppenScopes.Nemid, SignaturgruppenScopes.Mitid);
});

...
```
