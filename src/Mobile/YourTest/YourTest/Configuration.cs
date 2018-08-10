using System;
// NOTE: Do not remove. Used by generated code.
using YourTest.Helpers;

namespace YourTest
{
    // note: folow secrets initialization from https://github.com/dansiegel/Mobile.BuildTools/wiki/Build-Secrets
    // In VS4Mac Secrets can be treated as error but it all builds
    public static class Configuration
    {
        public static String AADClientId => Secrets.AzureClientId;
        public static String AADRedirectUri => Secrets.AzureRedirectUri;
        public static String AADAuthority { get; } = "https://login.windows.net/common";
        public static String AADResource => Secrets.AzureResource;
    }
}