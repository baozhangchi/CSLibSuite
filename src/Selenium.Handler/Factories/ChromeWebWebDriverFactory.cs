using System.IO;
using OpenQA.Selenium.Chrome;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;

namespace Selenium.Handler.Factories
{
    internal class ChromeWebWebDriverFactory : WebDriverFactory<ChromeDriver>
    {
        public override ChromeDriver CreateDriver(bool hideCommandPromptWindow = true, bool disableGpu = true, bool headLess = true,
            bool ignoreCertificateErrors = true, string driverPath = null)
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
            if (string.IsNullOrWhiteSpace(driverPath) || !File.Exists(driverPath))
            {
                driverPath =
                    Path.GetDirectoryName(new DriverManager().SetUpDriver(config, config.GetMatchingBrowserVersion()));
            }
            var driverService = ChromeDriverService.CreateDefaultService(driverPath);
            driverService.HideCommandPromptWindow = hideCommandPromptWindow;
            return new ChromeDriver(driverService, options);
        }
    }
}