using System;
using System.Collections.Generic;
using System.Management.Automation;
#if NET5_0_OR_GREATER
using System.Threading.Tasks;
#else
using System.Collections.ObjectModel;
#endif

namespace PowershellHost
{
    // <summary>
    /// Contains functionality for executing PowerShell scripts.
    /// </summary>
    public class SimpleHostedRunspace : IDisposable
    {
        private static SimpleHostedRunspace _instance;
        private PowerShell _powerShell;
        private static readonly object _lock = new object();

        public static SimpleHostedRunspace Default
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lock)
                    {
                        if (_instance == null)
                        {
                            _instance = new SimpleHostedRunspace();
                        }
                    }
                }

                return _instance;
            }
        }

        private SimpleHostedRunspace()
        {
            _powerShell = PowerShell.Create();
        }


        /// <summary>
        /// Runs a PowerShell script with parameters and prints the resulting pipeline objects to the console output. 
        /// </summary>
        /// <param name="scriptContents">The script file contents.</param>
        /// <param name="scriptParameters">A dictionary of parameter names and parameter values.</param>
#if NET5_0_OR_GREATER
        public async Task<PSDataCollection<PSObject>> RunScript(string scriptContents, Dictionary<string, object> scriptParameters = null)
#else
        public Collection<PSObject> RunScript(string scriptContents, Dictionary<string, object> scriptParameters = null)
#endif
        {
            // create a new hosted PowerShell instance using the default runspace.
            // wrap in a using statement to ensure resources are cleaned up.

            using (PowerShell ps = PowerShell.Create())
            {
                // specify the script code to run.
                ps.AddScript(scriptContents);

                // specify the parameters to pass into the script.
                if (scriptParameters != null)
                {
                    ps.AddParameters(scriptParameters);
                }

                // execute the script and await the result.
#if NET5_0_OR_GREATER
                return await ps.InvokeAsync();
#else
                return ps.Invoke();
#endif
            }
        }

        public void Dispose()
        {
            _powerShell.Dispose();
        }
    }
}
