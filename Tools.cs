using Microsoft.Win32.SafeHandles;
using System;
using System.IO;
using System.Runtime.InteropServices;

namespace GogInstaller
{
    internal static class Tools
    {
        //=========================================
        [DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool DeleteFile(string lpFileName);

        public static void UnblockFile(string filePath)
        {
            // Remove Read-Only
            var fileInfo = new FileInfo(filePath);
            if (fileInfo.IsReadOnly)
            {
                fileInfo.IsReadOnly = false;
            }

            // Unblock file
            if (IsFileBlocked(filePath))
            {
                // The "Blocked" status is stored in a hidden stream attached to the file
                string zoneStream = filePath + ":Zone.Identifier";
                DeleteFile(zoneStream);
            }
        }

        //=========================================
        [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        private static extern SafeFileHandle CreateFile(
            string lpFileName,
            uint dwDesiredAccess,
            uint dwShareMode,
            IntPtr lpSecurityAttributes,
            uint dwCreationDisposition,
            uint dwFlagsAndAttributes,
            IntPtr hTemplateFile);

        private const uint GENERIC_READ = 0x80000000;
        private const uint FILE_SHARE_READ = 1;
        private const uint OPEN_EXISTING = 3;

        private static bool IsFileBlocked(string filePath)
        {
            // The "Block" is stored in this specific hidden stream
            string zoneStream = filePath + ":Zone.Identifier";

            // Attempt to open the stream
            using (SafeFileHandle handle = CreateFile(
                zoneStream,
                GENERIC_READ,
                FILE_SHARE_READ,
                IntPtr.Zero,
                OPEN_EXISTING,
                0,
                IntPtr.Zero))
            {
                // If the handle is valid, the stream exists, meaning the file is blocked
                return !handle.IsInvalid;
            }
        }
    }
}
