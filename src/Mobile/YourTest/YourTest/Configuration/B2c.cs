using System;
// NOTE: Do not remove. Used by generated code.
using YourTest.Helpers;

namespace YourTest.Configuration
{
    // note: folow secrets initialization from https://github.com/dansiegel/Mobile.BuildTools/wiki/Build-Secrets
    // In VS4Mac Secrets can be treated as error but it all builds
    public static class B2c
    {
        public static String ClientId => Secrets.B2cClientId;
        public static String RedirectUri => Secrets.B2cRedirectUri;
        public static String Authority => Secrets.B2cAuthority;
        public static String ApiScopes => Secrets.B2cScopes;

    }
}