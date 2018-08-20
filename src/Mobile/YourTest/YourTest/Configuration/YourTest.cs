using System;
using YourTest.Helpers;

namespace YourTest.Configuration
{
    // note: folow secrets initialization from https://github.com/dansiegel/Mobile.BuildTools/wiki/Build-Secrets
    // In VS4Mac Secrets can be treated as error but it all builds
    public static class YourTest
    {
        public static String RestEndpoint => Secrets.RestEndpoint;
    }
}
