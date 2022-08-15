using System;
using System.Text;
using System.Runtime.InteropServices;

namespace Маркировка_резисторов
{
    internal class IniFile
    {
        private readonly string fileName = ".\\MRDat.ini";

        public void Write<T>(string Section, string Key, T Value) where T : IConvertible
        {
            SafeNativeMethods.WritePrivateProfileString(Section, Key, Value.ToString(), fileName);
        }

        public int Parse(string Section, string Key)
        {
            StringBuilder result = new StringBuilder(60);
            SafeNativeMethods.GetPrivateProfileString(Section, Key, null, result, result.Capacity, fileName);
            return int.Parse(result.ToString());
        }

        private static class SafeNativeMethods
        {
            [DllImport("kernel32.dll", EntryPoint = "WritePrivateProfileString", CharSet = CharSet.Unicode)]
            internal static extern int WritePrivateProfileString(string Section, string Key, string Value, string File);

            [DllImport("kernel32.dll", CharSet = CharSet.Unicode, EntryPoint = "GetPrivateProfileString")]
            internal static extern int GetPrivateProfileString(string Section, string Key, string Default, StringBuilder Result, int Size, string FileName);
        }
    }
}