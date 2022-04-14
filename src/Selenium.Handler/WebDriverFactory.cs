using OpenQA.Selenium;
using Selenium.Handler.Factories;

namespace Selenium.Handler
{
    public abstract class WebDriverFactory<T> : IDriverFactory<T>
        where T : WebDriver
    {
        /// <summary>
        /// 创建WebDriver
        /// </summary>
        /// <param name="driverType">Driver类型</param>
        /// <param name="hideCommandPromptWindow">隐藏黑框</param>
        /// <param name="disableGpu">禁用GPU加速</param>
        /// <param name="headLess">无头模式</param>
        /// <param name="ignoreCertificateErrors">忽略证书错误</param>
        /// <returns></returns>
        // ReSharper disable once UnusedMember.Global
        public static WebDriver CreateDriver(DriverType driverType, bool hideCommandPromptWindow = true, bool disableGpu = true, bool headLess = true, bool ignoreCertificateErrors = true)
        {
            switch (driverType)
            {
                case DriverType.Chrome:
                    return new ChromeWebWebDriverFactory().CreateDriver(hideCommandPromptWindow, disableGpu, headLess,
                        ignoreCertificateErrors);
                case DriverType.Edge:
                    return new EdgeWebWebDriverFactory().CreateDriver(hideCommandPromptWindow, disableGpu, headLess,
                        ignoreCertificateErrors);
                case DriverType.Firefox:
                    return new FirefoxWebWebDriverFactory().CreateDriver(hideCommandPromptWindow, disableGpu, headLess,
                        ignoreCertificateErrors);

            }

            return default;
        }

        public abstract T CreateDriver(bool hideCommandPromptWindow = true, bool disableGpu = true, bool headLess = true,
            bool ignoreCertificateErrors = true);
    }

    public interface IDriverFactory<out T>
    where T : WebDriver
    {
        // ReSharper disable once UnusedMemberInSuper.Global
        T CreateDriver(bool hideCommandPromptWindow = true, bool disableGpu = true, bool headLess = true, bool ignoreCertificateErrors = true);
    }

    public enum DriverType
    {
        Chrome,
        Firefox,
        Edge
    }
}
