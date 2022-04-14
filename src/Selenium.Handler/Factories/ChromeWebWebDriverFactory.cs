using System.IO;
using OpenQA.Selenium.Chrome;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;

namespace Selenium.Handler.Factories
{
    internal class ChromeWebWebDriverFactory : WebDriverFactory<ChromeDriver>
    {
        public override ChromeDriver CreateDriver(bool hideCommandPromptWindow = true, bool disableGpu = true, bool headLess = true,
            bool ignoreCertificateErrors = true)
        {
            var options = new ChromeOptions();
            if (disableGpu)
            {
                options.AddArgument("--disable-gpu");
            }

            if (headLess)
            {
                options.AddArgument("--headless");
            }

            if (ignoreCertificateErrors)
            {
                options.AddArgument("ignore-certificate-errors");
            }

            var config = new ChromeConfig();
            var driverService = ChromeDriverService.CreateDefaultService(Path.GetDirectoryName(new DriverManager().SetUpDriver(config, config.GetMatchingBrowserVersion())));
            driverService.HideCommandPromptWindow = hideCommandPromptWindow;
            return new ChromeDriver(driverService, options);
        }
    }
}