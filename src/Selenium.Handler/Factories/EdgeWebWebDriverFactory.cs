using System.IO;
using OpenQA.Selenium.Edge;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;

namespace Selenium.Handler.Factories
{
    internal class EdgeWebWebDriverFactory : WebDriverFactory<EdgeDriver>
    {
        public override EdgeDriver CreateDriver(bool hideCommandPromptWindow = true, bool disableGpu = true, bool headLess = true,
            bool ignoreCertificateErrors = true, string driverPath = null)
        {
            var options = new EdgeOptions();
            if (disableGpu)
            {
            }

            var config = new EdgeConfig();
            if (string.IsNullOrWhiteSpace(driverPath) || !File.Exists(driverPath))
            {
                driverPath =
                    Path.GetDirectoryName(new DriverManager().SetUpDriver(config, config.GetMatchingBrowserVersion()));
            }
            var driverService = EdgeDriverService.CreateDefaultService(driverPath);
            driverService.HideCommandPromptWindow = hideCommandPromptWindow;
            return new EdgeDriver(driverService, options);
        }
    }
}