#if NET5_0_OR_GREATER
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Management.Automation.Runspaces;
using System.Text;
using System.Threading.Tasks;

namespace PowershellHost
{
    /// <summary>
    /// Contains functionality for executing PowerShell scripts.
    /// </summary>
    public class CustomHostedRunspace
    {
        private static CustomHostedRunspace _instance;
        private PowerShell _powerShell;
        private static readonly object _lock = new object();

        public static CustomHostedRunspace Default
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lock)
                    {
                        if (_instance == null)
                        {
                            _instance = new CustomHostedRunspace();
                        }
                    }
                }

                return _instance;
            }
        }

        private CustomHostedRunspace()
        {
            _powerShell = PowerShell.Create();

            // subscribe to events from some of the streams
            _powerShell.Streams.Error.DataAdded += Error_DataAdded;
            _powerShell.Streams.Warning.DataAdded += Warning_DataAdded;
            _powerShell.Streams.Information.DataAdded += Information_DataAdded;
        }

        /// <summary>
        /// The PowerShell runspace pool.
        /// </summary>
        private RunspacePool RsPool { get; set; }

        /// <summary>
        /// Initialize the runspace pool.
        /// </summary>
        /// <param name="minRunspaces"></param>
        /// <param name="maxRunspaces"></param>
        public void InitializeRunspaces(int minRunspaces, int maxRunspaces, params string[] modulesToLoad)
        {
            // create the default session state.
            // session state can be used to set things like execution policy, language constraints, etc.
            // optionally load any modules (by name) that were supplied.

            var defaultSessionState = InitialSessionState.CreateDefault();
            defaultSessionState.ExecutionPolicy = Microsoft.PowerShell.ExecutionPolicy.Unrestricted;

            foreach (var moduleName in modulesToLoad)
            {
                defaultSessionState.ImportPSModule(moduleName);
            }

            // use the runspace factory to create a pool of runspaces
            // with a minimum and maximum number of runspaces to maintain.

            RsPool = RunspaceFactory.CreateRunspacePool(defaultSessionState);
            RsPool.SetMinRunspaces(minRunspaces);
            RsPool.SetMaxRunspaces(maxRunspaces);

            // set the pool options for thread use.
            // we can throw away or re-use the threads depending on the usage scenario.

            RsPool.ThreadOptions = PSThreadOptions.UseNewThread;

            // open the pool. 
            // this will start by initializing the minimum number of runspaces.

            RsPool.Open();
        }

        /// <summary>
        /// Runs a PowerShell script with parameters and prints the resulting pipeline objects to the console output. 
        /// </summary>
        /// <param name="command">The script file contents.</param>
        /// <param name="scriptParameters">A dictionary of parameter names and parameter values.</param>
        public async Task<PSDataCollection<PSObject>> RunCommandAsync(string command, Dictionary<string, object> scriptParameters = null)
        {
            if (RsPool == null)
            {
                throw new ApplicationException("Runspace Pool must be initialized before calling RunScript().");
            }

            // create a new hosted PowerShell instance using a custom runspace.
            // wrap in a using statement to ensure resources are cleaned up.

            //using (PowerShell _powerShell = PowerShell.Create())
            {
                // use the runspace pool.
                _powerShell.RunspacePool = RsPool;

                _powerShell.Commands.Clear();

                // specify the script code to run.
                _powerShell.AddCommand(command);

                // specify the parameters to pass into the script.
                if (scriptParameters != null && scriptParameters.Count > 0)
                {
                    _powerShell.AddParameters(scriptParameters);
                }

                // execute the script and await the result.
                return await _powerShell.InvokeAsync();
            }
        }

        /// <summary>
        /// Runs a PowerShell script with parameters and prints the resulting pipeline objects to the console output. 
        /// </summary>
        /// <param name="scriptContents">The script file contents.</param>
        /// <param name="scriptParameters">A dictionary of parameter names and parameter values.</param>
        public async Task<PSDataCollection<PSObject>> RunScriptAsync(string scriptContents)
        {
            if (RsPool == null)
            {
                throw new ApplicationException("Runspace Pool must be initialized before calling RunScript().");
            }

            // create a new hosted PowerShell instance using a custom runspace.
            // wrap in a using statement to ensure resources are cleaned up.

            //using (PowerShell _powerShell = PowerShell.Create())
            {
                // use the runspace pool.
                _powerShell.RunspacePool = RsPool;

                _powerShell.Commands.Clear();

                // specify the script code to run.
                _powerShell.AddScript(scriptContents);

                // execute the script and await the result.
                return await _powerShell.InvokeAsync();
            }
        }

        public event EventHandler<PSDataAddedArgs<ErrorRecord>> ErrorDataAdded;
        public event EventHandler<PSDataAddedArgs<WarningRecord>> WarningDataAdded;
        public event EventHandler<PSDataAddedArgs<InformationRecord>> InformationDataAdded;

        /// <summary>
        /// Handles data-added events for the information stream.
        /// </summary>
        /// <remarks>
        /// Note: Write-Host and Write-Information messages will end up in the information stream.
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Information_DataAdded(object sender, DataAddedEventArgs e)
        {
            var streamObjectsReceived = sender as PSDataCollection<InformationRecord>;
            var currentStreamRecord = streamObjectsReceived[e.Index];

            InformationDataAdded?.Invoke(this, new PSDataAddedArgs<InformationRecord>(currentStreamRecord));
        }

        /// <summary>
        /// Handles data-added events for the warning stream.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Warning_DataAdded(object sender, DataAddedEventArgs e)
        {
            var streamObjectsReceived = sender as PSDataCollection<WarningRecord>;
            var currentStreamRecord = streamObjectsReceived[e.Index];

            WarningDataAdded?.Invoke(this, new PSDataAddedArgs<WarningRecord>(currentStreamRecord));
        }

        /// <summary>
        /// Handles data-added events for the error stream.
        /// </summary>
        /// <remarks>
        /// Note: Uncaught terminating errors will stop the pipeline completely.
        /// Non-terminating errors will be written to this stream and execution will continue.
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Error_DataAdded(object sender, DataAddedEventArgs e)
        {
            var streamObjectsReceived = sender as PSDataCollection<ErrorRecord>;
            var currentStreamRecord = streamObjectsReceived[e.Index];

            ErrorDataAdded?.Invoke(this, new PSDataAddedArgs<ErrorRecord>(currentStreamRecord));
        }
    }
}

#endif