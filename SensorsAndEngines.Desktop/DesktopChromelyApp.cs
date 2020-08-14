namespace SensorsAndEngines.Desktop
{
    using Chromely;
    using Chromely.Core;

    public class DesktopChromelyApp : ChromelyBasicApp
    {
        public override void Configure(IChromelyContainer container)
        {
            base.Configure(container);
            base.Configuration.StartUrl = $"localhost:{WebApplication.Program.Port}";
#if DEBUG
#else
            base.Configuration.CefDownloadOptions.DownloadSilently = true;
#endif
        }
    }
}