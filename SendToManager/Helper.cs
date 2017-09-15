using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.IO;

namespace SendToManager
{
    class Helper
    {
        readonly static string GUID = "1455BDE4-ECAA-4ECC-B479-992386C7EC12";
        internal static bool WriteAlternateStream(string file, string data)
        {
            byte[] b = Encoding.UTF8.GetBytes(data);
            return Ambiesoft.CppUtils.WriteAlternate(file, GUID, b);
        }
        internal static bool ReadAlternateStream(string file, out string data)
        {
            data = null;
            byte[] b = null;
            if (!Ambiesoft.CppUtils.ReadAlternate(file, GUID, ref b))
                return false;

            data = Encoding.UTF8.GetString(b);
            return true;
        }
    }

}
