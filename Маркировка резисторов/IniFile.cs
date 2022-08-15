using System;
using System.IO;
using System.Text;
using System.Runtime.InteropServices;

namespace Маркировка_резисторов
{
    internal class IniFile
    {
        private const string _name = "MRDat.ini";
        private readonly string _fileName = Path.Combine(Environment.CurrentDirectory, _name);

        public void Write<T>(string Section, string Key, T Value) where T : IConvertible
        {
            SafeNativeMethods.WritePrivateProfileString(Section, Key, Value.ToString(), _fileName);
        }

        public int Parse(string Section, string Key)
        {
            StringBuilder result = new StringBuilder(60);
            SafeNativeMethods.GetPrivateProfileString(Section, Key, null, result, result.Capacity, _fileName);
            return int.Parse(result.ToString());
        }

        private static class SafeNativeMethods
        {
            [DllImport("kernel32.dll", EntryPoint = "WritePrivateProfileString", CharSet = CharSet.Unicode)]
            internal static extern int WritePrivateProfileString(string Section, string Key, string Value, string fileName);

            [DllImport("kernel32.dll", CharSet = CharSet.Unicode, EntryPoint = "GetPrivateProfileString")]
            internal static extern int GetPrivateProfileString(string Section, string Key, string Default, StringBuilder Result, int Size, string fileName);
        }
    }
}