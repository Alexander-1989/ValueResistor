using System;
using System.Runtime.InteropServices;

namespace Маркировка_резисторов
{
    class IniFile
    {
        readonly string FilePath = ".\\MRDat.ini";

        public void Write<T>(string Section, string Key, T Value) where T : IConvertible
        {
            SafeNativeMethods.WritePrivateProfileString(Section, Key, Value.ToString(), FilePath);
        }

        public int Parse(string Section, string Key)
        {
            char[] res = new char[5];
            SafeNativeMethods.GetPrivateProfileString(Section, Key, null, res, res.Length, FilePath);
            return int.Parse(new string(res));
        }

        private static class SafeNativeMethods
        {
            [DllImport("kernel32.dll", EntryPoint = "WritePrivateProfileString", CharSet = CharSet.Unicode)]
            internal static extern int WritePrivateProfileString(string Section, string Key, string Value, string File);

            [DllImport("kernel32.dll", EntryPoint = "GetPrivateProfileString", CharSet = CharSet.Unicode)]
            internal static extern int GetPrivateProfileString(string Section, string Key, string Def, char[] Result, int Size, string File);
        }
    }
}