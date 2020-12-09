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

        public static Dictionary<string, int> AsmValues { get; set; } = new Dictionary<string, int>();
        //public static string Path = "";

        /// <summary>
        /// 查看遊戲是否還在執行
        /// </summary>
        public static bool IsOpen { get => MsProc != null && !MsProc.HasExited; }

        /// <summary>
        /// 遊戲的ID
        /// </summary>
        public static int Pid { get => MsProc.Id;  }


        private static object asmLock = new object();
        private static int asmId = 0;


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


        /// <summary>
        /// 重設script
        /// </summary>
        /// <param name="name"></param>
        /// <param name="script"></param>
        /// <returns></returns>
        public static bool ResetScript(string name, string script)
        {
            if (AsmValues.ContainsKey(name))
            {
                lock (asmLock)
                {
                    int val = AsmValues[name];
                    CheatEngine.CheatEngineLibrary.iRemoveRecord.Invoke(val);
                    CheatEngine.CheatEngineLibrary.iAddScript.Invoke(name, script);
                    if(AsmValues.Max(x => x.Value) != val)
                    {
                        AsmValues[name] = asmId;
                        asmId++;
                    }
                }
                return true;
            }
            return false;
        }

        /// <summary>
        /// 新增script
        /// </summary>
        /// <param name="name"></param>
        /// <param name="script"></param>
        /// <returns></returns>
        public static bool AddScript(string name, String script)
        {
            if (!AsmValues.ContainsKey(name))
            {
                lock (asmLock)
                {
                    int max = AsmValues.Select(x => x.Value).DefaultIfEmpty(0).Max();
                    int tempID = -1;
                    for (int i = 0; i < max; i++)
                    {
                        if (!AsmValues.ContainsValue(i))
                            tempID = i;
                        break;
                    }
                    if (tempID != -1)
                    {
                        AsmValues.Add(name, tempID);
                        CheatEngine.CheatEngineLibrary.iAddScript.Invoke(name, script);
                    }
                    else
                    {
                        AsmValues.Add(name, asmId);
                        CheatEngine.CheatEngineLibrary.iAddScript.Invoke(name, script);
                        asmId++;
                    }
                }
                return true;
            }

            else return false;

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="enable"></param>
        /// <returns></returns>
        public static bool CeAutoAsm(string name, bool enable)
        {
            if (MsProc == null || MsProc.HasExited) return false;

            if (AsmValues.ContainsKey(name))
            {
                lock (asmLock)
                {
                    CheatEngine.CheatEngineLibrary.iActivateRecord.Invoke(AsmValues[name], enable);
                }
                return true;
            }
            else return false;


        }

        /// <summary>
        /// 結束遊戲/程式
        /// </summary>
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


        private static bool CeAutoAsmLoad()
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
                    AsmValues.Clear();
                    asmId = 0;
                }
                CheatEngine.CheatEngineLibrary.iOpenProcess.Invoke(Convert.ToString(MsProc.Id, 16));
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
            AsmValues.Clear();
            asmId = 0;
            MsProc = null;
            CheatEngine.CheatEngineLibrary.unloadEngine();
        }
    };
}
