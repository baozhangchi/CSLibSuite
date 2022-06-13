using System;
using OpenQA.Selenium;
using Selenium.Handler.Factories;

namespace Selenium.Handler
{
    /// <summary>
    /// WebDriver工厂类
    /// </summary>
    public static class WebDriverFactory
    {
        /// <summary>
        /// 创建WebDriver
        /// </summary>
        /// <param name="driverType">Driver类型</param>
        /// <param name="hideCommandPromptWindow">隐藏黑框</param>
        /// <param name="disableGpu">禁用GPU加速</param>
        /// <param name="headLess">无头模式</param>
        /// <param name="ignoreCertificateErrors">忽略证书错误</param>
        /// <param name="driverPath">驱动路径</param>
        /// <returns></returns>
        // ReSharper disable once UnusedMember.Global
        public static WebDriver CreateDriver(DriverType driverType, bool hideCommandPromptWindow = true, bool disableGpu = true, bool headLess = true, bool ignoreCertificateErrors = true, string driverPath = null)
        {
            switch (driverType)
            {
                case DriverType.Chrome:
                    return new ChromeWebWebDriverFactory().CreateDriver(hideCommandPromptWindow, disableGpu, headLess,
                        ignoreCertificateErrors, driverPath);
                case DriverType.Edge:
                    return new EdgeWebWebDriverFactory().CreateDriver(hideCommandPromptWindow, disableGpu, headLess,
                        ignoreCertificateErrors, driverPath);
                case DriverType.Firefox:
                    return new FirefoxWebWebDriverFactory().CreateDriver(hideCommandPromptWindow, disableGpu, headLess,
                        ignoreCertificateErrors, driverPath);

                default:
                    return default;
            }
        }

        /// <summary>
        /// 生成WebDriver
        /// </summary>
        /// <typeparam name="T">WebDriver工厂类类型</typeparam>
        /// <param name="hideCommandPromptWindow">隐藏黑框</param>
        /// <param name="disableGpu">禁用GPU加速</param>
        /// <param name="headLess">无头模式</param>
        /// <param name="ignoreCertificateErrors">忽略证书错误</param>
        /// <returns></returns>
        public static WebDriver GenerateWebDriver<T>(bool hideCommandPromptWindow = true, bool disableGpu = true, bool headLess = true, bool ignoreCertificateErrors = true)
        where T : IDriverFactory
        {
            return Activator.CreateInstance<T>().CreateDriver(hideCommandPromptWindow, disableGpu, headLess, ignoreCertificateErrors);
        }
    }

    /// <summary>
    /// WebDriver工厂类
    /// </summary>
    /// <typeparam name="T">WebDriver类型</typeparam>
    public abstract class WebDriverFactory<T> : IDriverFactory<T>
        where T : WebDriver
    {
        // ReSharper disable once EmptyConstructor
        protected WebDriverFactory()
        {
        }

        /// <summary>
        /// 生成WebDriver
        /// </summary>
        /// <param name="hideCommandPromptWindow">隐藏黑框</param>
        /// <param name="disableGpu">禁用GPU加速</param>
        /// <param name="headLess">无头模式</param>
        /// <param name="ignoreCertificateErrors">忽略证书错误</param>
        /// <param name="driverPath">驱动路径</param>
        /// <returns></returns>
        public abstract T CreateDriver(bool hideCommandPromptWindow = true, bool disableGpu = true, bool headLess = true,
            bool ignoreCertificateErrors = true, string driverPath = null);

        /// <summary>
        /// 生成WebDriver
        /// </summary>
        /// <param name="hideCommandPromptWindow">隐藏黑框</param>
        /// <param name="disableGpu">禁用GPU加速</param>
        /// <param name="headLess">无头模式</param>
        /// <param name="ignoreCertificateErrors">忽略证书错误</param>
        /// <param name="driverPath">驱动路径</param>
        /// <returns></returns>
        WebDriver IDriverFactory.CreateDriver(bool hideCommandPromptWindow, bool disableGpu, bool headLess,
            bool ignoreCertificateErrors, string driverPath)
        {
            return CreateDriver(hideCommandPromptWindow, disableGpu, headLess, ignoreCertificateErrors, driverPath);
        }
    }
}
