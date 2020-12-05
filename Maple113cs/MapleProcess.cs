using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessTools
{
    public static class MapleProcess
    {
        private static bool IsLoad = false;
        public static Process MsProc { get; private set; }

        public static Dictionary<string, int> asmValue { get; set; } = new Dictionary<string, int>();
        //public static string Path = "";

        /// <summary>
        /// 查看目標是某關閉
        /// </summary>
        public static bool IsOpen { get => MsProc != null && !MsProc.HasExited; }


        public static int Pid { get => MsProc.Id;  }


        public static object asmLock = new object();
        public static int asmId = 0;

        public static Memory.MemoryControl MemoryControl { get; set; }
        public static bool IsX64
        {
            get
            {
                if (MsProc == null || MsProc.HasExited || !API.IsWow64Process(MsProc.Handle, out bool isx64)) throw new Exception("Process error");
                return isx64;
            }
        }

        public static bool Start(String fileName, params object[] args)
        {
            try
            {
                MsProc?.Close();
                ProcessStartInfo info = new ProcessStartInfo(fileName)
                {
                    Arguments = ""
                };
                
                foreach (var i in args)
                {
                    info.Arguments += i + " ";
                }

                if (info.Arguments.Length > 1 && info.Arguments[info.Arguments.Length - 1] == ' ') info.Arguments = info.Arguments.Remove(info.Arguments.Length - 1);
                MsProc = Process.Start(info);
            }
            catch (Exception ex)
            {
                return false;
            }
            finally
            {
                //if (Handle != null) API.CloseHandle(Handle);
            }

            return CeAutoAsmLoad();
        }

        public static bool LoadFromPid(int pid)
        {
            try
            {
                MsProc?.Close();
                MsProc = Process.GetProcessById(pid);
            }
            catch (Exception ex)
            {
                return false;
            }
            finally
            {
                //if (Handle != null) API.CloseHandle(Handle);
            }

            return CeAutoAsmLoad();
        }

        public static bool ResetScript(string name, string script)
        {
            if (asmValue.ContainsKey(name))
            {
                lock (asmLock)
                {
                    int val = asmValue[name];
                    CheatEngine.CheatEngineLibrary.iRemoveRecord.Invoke(val);
                    CheatEngine.CheatEngineLibrary.iAddScript.Invoke(name, script);
                    if(asmValue.Max(x => x.Value) != val)
                    {
                        asmValue[name] = asmId;
                        asmId++;
                    }
                }
                return true;
            }
            return false;
        }
        public static bool CeAutoAsm(string name, String script, bool enable)
        {
            if (MsProc == null || MsProc.HasExited) return false;

            if (asmValue.ContainsKey(name))
            {
                lock (asmLock)
                {
                    CheatEngine.CheatEngineLibrary.iActivateRecord.Invoke(asmValue[name], enable);
                }
            }
            else
            {
                lock (asmLock)
                {
                    int max = asmValue.Select(x => x.Value).DefaultIfEmpty(0).Max();
                    int tempID = -1;
                    for(int i = 0; i < max; i++)
                    {
                        if (!asmValue.ContainsValue(i))
                            tempID = i;
                        break;
                    }
                    if(tempID != -1)
                    {
                        asmValue.Add(name, tempID);
                        CheatEngine.CheatEngineLibrary.iAddScript.Invoke(name, script);
                        CheatEngine.CheatEngineLibrary.iActivateRecord.Invoke(tempID, enable);
                    }
                    else
                    {
                        asmValue.Add(name, asmId);
                        CheatEngine.CheatEngineLibrary.iAddScript.Invoke(name, script);
                        CheatEngine.CheatEngineLibrary.iActivateRecord.Invoke(asmId, enable);
                        asmId++;
                    }
                }
                

            }


            return true;
        }
        public static void Kill() 
        {
            try
            {
                MsProc?.Kill();

            }
            catch
            {

            }
            MsProc = null;
        }


        /// <summary>
        /// 於程式開始時呼叫
        /// </summary>
        /// <returns></returns>
        public static bool CeAutoAsmLoad()
        {

            lock (asmLock)
            {
                if (!IsLoad)
                {
                    if (!Directory.Exists("lib"))
                    {
                        Directory.CreateDirectory("lib");
                    }
                    if (!File.Exists(Path.Combine("lib", "ce_lib32.dll")))
                        File.WriteAllBytes(Path.Combine("lib", "ce_lib32.dll"), ProcessTools.Properties.Resources.ce_lib32);
                    if (!File.Exists(Path.Combine("lib", "ce_lib64.dll")))
                        File.WriteAllBytes(Path.Combine("lib", "ce_lib64.dll"), ProcessTools.Properties.Resources.ce_lib64);
                    if (!CheatEngine.CheatEngineLibrary.loadEngine("lib")) return false;
                    IsLoad = true;
                }
                else
                {
                    CheatEngine.CheatEngineLibrary.iResetTable.Invoke();
                    asmValue.Clear();
                    asmId = 0;
                }
                CheatEngine.CheatEngineLibrary.iOpenProcess.Invoke(Convert.ToString(MsProc.Id, 16));//Convert.ToString(MsProc.Handle.ToInt32(), 16).ToUpper().PadLeft(8, '0'));
                if(MemoryControl == null) MemoryControl = new Memory.MemoryControl();
                MemoryControl.LoadFromPid(MsProc.Id);
            }
            
            return true;
        }

        /// <summary>
        /// 於程式結束時呼叫
        /// </summary>
        public static void Unload()
        {
            CheatEngine.CheatEngineLibrary.iResetTable.Invoke();
            asmValue.Clear();
            asmId = 0;
            MsProc = null;
            CheatEngine.CheatEngineLibrary.unloadEngine();


        }
    };
}
