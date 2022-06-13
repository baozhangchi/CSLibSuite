using System.IO;
using OpenQA.Selenium.Firefox;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;

namespace Selenium.Handler.Factories
{
    internal class FirefoxWebWebDriverFactory : WebDriverFactory<FirefoxDriver>
    {
        public override FirefoxDriver CreateDriver(bool hideCommandPromptWindow = true, bool disableGpu = true, bool headLess = true,
            bool ignoreCertificateErrors = true, string driverPath = null)
        {
            var options = new FirefoxOptions();
            if (disableGpu)
            {
            }

            var config = new FirefoxConfig();
            if (string.IsNullOrWhiteSpace(driverPath) || !File.Exists(driverPath))
            {
                driverPath =
                    Path.GetDirectoryName(new DriverManager().SetUpDriver(config, config.GetMatchingBrowserVersion()));
            }
            var driverService = FirefoxDriverService.CreateDefaultService(driverPath);
            driverService.HideCommandPromptWindow = hideCommandPromptWindow;
            return new FirefoxDriver(driverService, options);
        }
    }
}