using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace GuessTheSong
{
    public static class ProcessExecuter
    {
        public static void Execute(ProcessSettingsBase settings)
        {
            Process process = new Process();
            ProcessStartInfo info = new ProcessStartInfo();
            info.FileName = settings.ProcessName;
            info.Arguments = settings.GetParametersString();
            info.WorkingDirectory = Environment.CurrentDirectory;
            process.StartInfo = info;
            process.Start();
        }
    }
}
