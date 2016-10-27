using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace flood
{
    public static class ProcessCreator
    {
        /// <summary>
        /// Creates a process with default settings
        /// </summary>
        /// <param name="processType"></param>
        /// <returns>Process</returns>
        public static Process CreateProcess(string processType)
        {
            return Process.Start(new ProcessStartInfo()
            {
                FileName = processType,
                WorkingDirectory = @"C:\"
            });
        }

        public static Process CreateProcess(ProcessStartInfo processType)
        {
            return Process.Start(processType);
        }

        /// <summary>
        /// Creates a process with specified settings asyncronously
        /// </summary>
        /// <param name="processType"></param>
        /// <returns></returns>
        public static async Task<Process> CreateProcessAsync(ProcessStartInfo processType)
        {
            return await Task.Run(() =>
                {
                    return CreateProcess(processType);
                });
        }
    }
}
