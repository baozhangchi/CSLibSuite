using OpenQA.Selenium;

namespace Selenium.Handler
{
    /// <summary>
    /// WebDriver工厂类接口
    /// </summary>
    /// <typeparam name="T">WebDriver类型</typeparam>
    public interface IDriverFactory<out T> : IDriverFactory
        where T : WebDriver
    {
        /// <summary>
        /// 生成WebDriver
        /// </summary>
        /// <param name="hideCommandPromptWindow">隐藏黑框</param>
        /// <param name="disableGpu">禁用GPU加速</param>
        /// <param name="headLess">无头模式</param>
        /// <param name="ignoreCertificateErrors">忽略证书错误</param>
        /// <returns></returns>
        // ReSharper disable once UnusedMemberInSuper.Global
        new T CreateDriver(bool hideCommandPromptWindow = true, bool disableGpu = true, bool headLess = true, bool ignoreCertificateErrors = true);
    }

    /// <summary>
    /// WebDriver工厂类接口
    /// </summary>
    public interface IDriverFactory
    {
        /// <summary>
        /// 生成WebDriver
        /// </summary>
        /// <param name="hideCommandPromptWindow">隐藏黑框</param>
        /// <param name="disableGpu">禁用GPU加速</param>
        /// <param name="headLess">无头模式</param>
        /// <param name="ignoreCertificateErrors">忽略证书错误</param>
        /// <returns></returns>
        WebDriver CreateDriver(bool hideCommandPromptWindow = true, bool disableGpu = true, bool headLess = true,
            bool ignoreCertificateErrors = true);
    }
}