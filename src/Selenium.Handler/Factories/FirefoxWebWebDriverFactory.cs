using System.IO;
using OpenQA.Selenium.Firefox;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;

namespace Selenium.Handler.Factories
{
    internal class FirefoxWebWebDriverFactory : WebDriverFactory<FirefoxDriver>
    {
        public override FirefoxDriver CreateDriver(bool hideCommandPromptWindow = true, bool disableGpu = true, bool headLess = true,
            bool ignoreCertificateErrors = true)
        {
            var options = new FirefoxOptions();
            if (disableGpu)
            {
            }

            var config = new FirefoxConfig();
            var driverService = FirefoxDriverService.CreateDefaultService(Path.GetDirectoryName(new DriverManager().SetUpDriver(config, config.GetMatchingBrowserVersion())));
            driverService.HideCommandPromptWindow = hideCommandPromptWindow;
            return new FirefoxDriver(driverService, options);
        }
    }
}