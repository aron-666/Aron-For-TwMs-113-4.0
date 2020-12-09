using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessTools
{
    public abstract class IProcess
    {
        protected Process process { get; set; }
        public Process Process {
            get => process;
            set
            {
                process = value;
                IntPtr h = IntPtr.Zero;
                API.IsWow64Process(Handle, out var b);
                IsX64 = !b;
            }
        }
        public bool IsX64 { get; protected set; }

        public bool IsOpen { get => Process != null && !Process.HasExited; }

        public int Pid { get => Process.Id; }

        public IntPtr Handle { get => Process.Handle; }

        public bool Start(String fileName, params object[] args)
        {
            try
            {
                Process?.Close();
                ProcessStartInfo info = new ProcessStartInfo(fileName)
                {
                    Arguments = ""
                };

                foreach (var i in args)
                {
                    info.Arguments += i + " ";
                }

                if (info.Arguments.Length > 1 && info.Arguments[info.Arguments.Length - 1] == ' ') info.Arguments = info.Arguments.Remove(info.Arguments.Length - 1);
                Process = Process.Start(info);
            }
            catch (Exception ex)
            {
                return false;
            }
            finally
            {
            }

            return true;
        }

        public bool LoadFromPid(int pid)
        {
            try
            {
                Process?.Close();
                Process = Process.GetProcessById(pid);
            }
            catch (Exception ex)
            {
                return false;
            }
            finally
            {
                //if (Handle != null) API.CloseHandle(Handle);
            }

            return true;
        }

        public void Kill() => Process?.Kill();

    }
}
