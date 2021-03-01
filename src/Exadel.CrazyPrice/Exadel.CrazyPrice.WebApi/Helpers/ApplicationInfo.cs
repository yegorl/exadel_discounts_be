namespace Exadel.CrazyPrice.WebApi.Helpers
{
    public static class ApplicationInfo
    {
        public static string ApplicationName
        {
            get
            {
                var startupNamespace = typeof(Startup).Namespace;
                return startupNamespace!.Substring(startupNamespace.LastIndexOf('.', startupNamespace.LastIndexOf('.') - 1) + 1);
            }
        }
    }
}
