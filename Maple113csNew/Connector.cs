using System;
using System.IO;
using System.Runtime.InteropServices;

namespace CheatEngine
{

    public enum TScanOption { soUnknownValue = 0, soExactValue = 1, soValueBetween = 2, soBiggerThan = 3, soSmallerThan = 4, soIncreasedValue = 5, soIncreasedValueBy = 6, soDecreasedValue = 7, soDecreasedValueBy = 8, soChanged = 9, soUnchanged = 10, soCustom };
    public enum TScanType { stNewScan, stFirstScan, stNextScan };
    public enum TRoundingType { rtRounded = 0, rtExtremerounded = 1, rtTruncated = 2 };
    public enum TVariableType { vtByte = 0, vtWord = 1, vtDword = 2, vtQword = 3, vtSingle = 4, vtDouble = 5, vtString = 6, vtUnicodeString = 7, vtByteArray = 8, vtBinary = 9, vtAll = 10, vtAutoAssembler = 11, vtPointer = 12, vtCustom = 13, vtGrouped = 14, vtByteArrays = 15 }; //all ,grouped and MultiByteArray are special types
    public enum TCustomScanType { cstNone, cstAutoAssembler, cstCPP, cstDLLFunction };
    public enum TFastScanMethod { fsmNotAligned = 0, fsmAligned = 1, fsmLastDigits = 2 };
    public enum Tscanregionpreference { scanDontCare, scanExclude, scanInclude };

    public static class CheatEngineLibrary
    {

        // Static invocations.
        [DllImport("kernel32.dll", EntryPoint = "LoadLibraryW", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Winapi)]
        extern static IntPtr LoadLibraryW(String strLib);

        [DllImport("kernel32.dll")]
        extern static IntPtr LoadLibrary(String strLib);

        [DllImport("kernel32.dll")]
        extern static int FreeLibrary(IntPtr iModule);

        [DllImport("kernel32.dll", EntryPoint = "GetProcAddress", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Winapi)]
        extern static IntPtr GetProcAddress(IntPtr iModule, String strProcName);

        public delegate void IGetProcessList([MarshalAs(UnmanagedType.BStr)] out string processes);
        public delegate void IOpenProcess([MarshalAs(UnmanagedType.BStr)] string pid);

        public delegate void IResetTable();
        public delegate void IAddScript([MarshalAs(UnmanagedType.BStr)] string name, [MarshalAs(UnmanagedType.BStr)] string script);
        public delegate void IActivateRecord(int id, bool activate);
        public delegate void IRemoveRecord(int id);
        public delegate void IApplyFreeze();

        public delegate void IAddAddressManually([MarshalAs(UnmanagedType.BStr)] string initialaddress,
        TVariableType vartype);
        public delegate void IGetValue(int id, [MarshalAs(UnmanagedType.BStr)] out string  value);
        public delegate void ISetValue(int id, [MarshalAs(UnmanagedType.BStr)] string value, bool freezer);
        public delegate void IProcessAddress([MarshalAs(UnmanagedType.BStr)] string address, TVariableType vartype,
         bool showashexadecimal, bool showAsSigned, int bytesize, [MarshalAs(UnmanagedType.BStr)] out string value);

        public delegate void IInitMemoryScanner(int handle);
        public delegate void INewScan();
        public delegate void IConfigScanner(Tscanregionpreference scanWritable, Tscanregionpreference scanExecutable, Tscanregionpreference scanCopyOnWrite);

        public delegate void IFirstScan(TScanOption scanOption, TVariableType variableType,
  TRoundingType roundingtype, [MarshalAs(UnmanagedType.BStr)] string scanvalue1,
   [MarshalAs(UnmanagedType.BStr)] string scanvalue2, [MarshalAs(UnmanagedType.BStr)] string startaddress,
   [MarshalAs(UnmanagedType.BStr)] string stopaddress, bool hexadecimal, bool binaryStringAsDecimal,
   bool unicode, bool casesensitive, TFastScanMethod fastscanmethod,
   [MarshalAs(UnmanagedType.BStr)] string fastscanparameter);

        public delegate void INextScan(TScanOption scanOption,TRoundingType roundingtype, [MarshalAs(UnmanagedType.BStr)] string scanvalue1,
 [MarshalAs(UnmanagedType.BStr)] string scanvalue2, bool hexadecimal, bool binaryStringAsDecimal,
 bool unicode, bool casesensitive, bool percentage, bool compareToSavedScan, [MarshalAs(UnmanagedType.BStr)] string savedscanname);

        public delegate Int64 ICountAddressesFound();
        public delegate void IGetAddress(Int64 index, [MarshalAs(UnmanagedType.BStr)] out string address, [MarshalAs(UnmanagedType.BStr)] out string value);
        public delegate void IInitFoundList(TVariableType vartype, int varlength, bool hexadecimal, bool signed, bool binaryasdecimal, bool unicode);
        public delegate void IResetValues();
        public delegate int IGetBinarySize();

        private static IntPtr libInst;

        public static IGetProcessList iGetProcessList;
        public static IOpenProcess iOpenProcess;

        public static IResetTable iResetTable;
        public static IAddScript iAddScript;
        public static IRemoveRecord iRemoveRecord;
        public static IActivateRecord iActivateRecord;
        public static IApplyFreeze iApplyFreeze;

        public static IAddAddressManually iAddAddressManually;
        public static IGetValue iGetValue;
        public static ISetValue iSetValue;
        public static IProcessAddress iProcessAddress;


        public static IInitMemoryScanner iInitMemoryScanner;
        public static INewScan iNewScan;
        public static IConfigScanner iConfigScanner;
        public static IFirstScan iFirstScan;
        public static INextScan iNextScan;
        public static ICountAddressesFound iCountAddressesFound;
        public static IGetAddress iGetAddress;
        public static IInitFoundList iInitFoundList;
        public static IResetValues iResetValues;
        public static IGetBinarySize iGetBinarySize;
        private static void loadFunctions()
        {
            IntPtr pGetProcessList = GetProcAddress(libInst, "IGetProcessList");
            IntPtr pOpenProcess = GetProcAddress(libInst, "IOpenProcess");

            IntPtr pResetTable = GetProcAddress(libInst, "IResetTable");
            IntPtr pAddScript = GetProcAddress(libInst, "IAddScript");
            IntPtr pRemoveRecord = GetProcAddress(libInst, "IRemoveRecord");
            IntPtr pActivateRecord = GetProcAddress(libInst, "IActivateRecord");
            IntPtr pApplyFreeze = GetProcAddress(libInst, "IApplyFreeze");

            IntPtr pAddAddressManually = GetProcAddress(libInst, "IAddAddressManually");
            IntPtr pGetValue = GetProcAddress(libInst, "IGetValue");
            IntPtr pSetValue = GetProcAddress(libInst, "ISetValue");
            IntPtr pProcessAddress = GetProcAddress(libInst, "IProcessAddress");

            IntPtr pInitMemoryScanner = GetProcAddress(libInst, "IInitMemoryScanner");
            IntPtr pNewScan = GetProcAddress(libInst, "INewScan");
            IntPtr pConfigScanner = GetProcAddress(libInst, "IConfigScanner");
            IntPtr pFirstScan = GetProcAddress(libInst, "IFirstScan");
            IntPtr pNextScan = GetProcAddress(libInst, "INextScan");
            IntPtr pCountAddressesFound = GetProcAddress(libInst, "ICountAddressesFound");
            IntPtr pGetAddress = GetProcAddress(libInst, "IGetAddress");
            IntPtr pInitFoundList = GetProcAddress(libInst, "IInitFoundList");
            IntPtr pResetValues = GetProcAddress(libInst, "IResetValues");
            IntPtr pGetBinarySize = GetProcAddress(libInst, "IGetBinarySize");

            iGetProcessList = (IGetProcessList)Marshal.GetDelegateForFunctionPointer(pGetProcessList, typeof(IGetProcessList));
            iOpenProcess = (IOpenProcess)Marshal.GetDelegateForFunctionPointer(pOpenProcess, typeof(IOpenProcess));
            
            iResetTable = (IResetTable)Marshal.GetDelegateForFunctionPointer(pResetTable, typeof(IResetTable));
            iAddScript = (IAddScript)Marshal.GetDelegateForFunctionPointer(pAddScript, typeof(IAddScript));
            iRemoveRecord = (IRemoveRecord)Marshal.GetDelegateForFunctionPointer(pRemoveRecord, typeof(IRemoveRecord));
            iActivateRecord = (IActivateRecord)Marshal.GetDelegateForFunctionPointer(pActivateRecord, typeof(IActivateRecord));
            iApplyFreeze = (IApplyFreeze)Marshal.GetDelegateForFunctionPointer(pApplyFreeze, typeof(IApplyFreeze));

            iAddAddressManually = (IAddAddressManually)Marshal.GetDelegateForFunctionPointer(pAddAddressManually, typeof(IAddAddressManually));
            iGetValue = (IGetValue)Marshal.GetDelegateForFunctionPointer(pGetValue, typeof(IGetValue));
            iSetValue = (ISetValue)Marshal.GetDelegateForFunctionPointer(pSetValue, typeof(ISetValue));
            iProcessAddress = (IProcessAddress)Marshal.GetDelegateForFunctionPointer(pProcessAddress, typeof(IProcessAddress));

            iInitMemoryScanner = (IInitMemoryScanner)Marshal.GetDelegateForFunctionPointer(pInitMemoryScanner, typeof(IInitMemoryScanner));
            iNewScan = (INewScan)Marshal.GetDelegateForFunctionPointer(pNewScan, typeof(INewScan));
            iConfigScanner = (IConfigScanner)Marshal.GetDelegateForFunctionPointer(pConfigScanner, typeof(IConfigScanner));
            iFirstScan = (IFirstScan)Marshal.GetDelegateForFunctionPointer(pFirstScan, typeof(IFirstScan));
            iNextScan = (INextScan)Marshal.GetDelegateForFunctionPointer(pNextScan, typeof(INextScan));

            iCountAddressesFound = (ICountAddressesFound)Marshal.GetDelegateForFunctionPointer(pCountAddressesFound, typeof(ICountAddressesFound));
            iGetAddress = (IGetAddress)Marshal.GetDelegateForFunctionPointer(pGetAddress, typeof(IGetAddress));
            iInitFoundList = (IInitFoundList)Marshal.GetDelegateForFunctionPointer(pInitFoundList, typeof(IInitFoundList));
            iResetValues = (IResetValues)Marshal.GetDelegateForFunctionPointer(pResetValues, typeof(IResetValues));
            iGetBinarySize = (IGetBinarySize)Marshal.GetDelegateForFunctionPointer(pGetBinarySize, typeof(IGetBinarySize));
        }
        public static bool loadEngine(string path)
        {
            if(Environment.Is64BitProcess) libInst = LoadLibraryW(Path.Combine(path, "ce_lib64.dll"));
            else libInst = LoadLibrary(Path.Combine(path, "ce_lib32.dll"));
            if (libInst != IntPtr.Zero)
            {
                
                loadFunctions();
                return true;
            }
            return false;
        }

        public static void unloadEngine()
        {
            if(libInst != IntPtr.Zero) FreeLibrary(libInst);
        }

    }

}

