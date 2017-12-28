using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace Ambiesoft.RegexFilenameRenamer
{
    //[System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential)]
    //public struct HWND__
    //{
    //    /// int
    //    public int unused;
    //}

    public partial class NativeMethods
    {
        internal const int WM_USER = 0x0400;
        internal const int IMF_DUALFONT = 0x0080;
        internal const int EM_GETLANGOPTIONS = (NativeMethods.WM_USER + 121);
        internal const int EM_SETLANGOPTIONS = (NativeMethods.WM_USER + 120);

        /// Return Type: LRESULT->LONG_PTR->int
        ///hWnd: HWND->HWND__*
        ///Msg: UINT->unsigned int
        ///wParam: WPARAM->UINT_PTR->unsigned int
        ///lParam: LPARAM->LONG_PTR->int
        //[System.Runtime.InteropServices.DllImportAttribute("user32.dll", EntryPoint = "SendMessageW")]
        //[return: System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.SysInt)]
        //public static extern int SendMessageW([System.Runtime.InteropServices.InAttribute()] System.IntPtr hWnd, uint Msg, [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.SysUInt)] uint wParam, [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.SysInt)] int lParam);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int SendMessage(IntPtr hWnd, UInt32 Msg, int wParam, int lParam);
    }
}
