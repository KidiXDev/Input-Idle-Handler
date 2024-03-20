using System;
using System.Runtime.InteropServices;

namespace idh
{
    internal struct LASTINPUTINFO
    {
        public uint cbSize;
        public uint dwTime;
    }

    public class IdleHandler
    {

        [DllImport("User32.dll")]
        private static extern bool GetLastInputInfo(ref LASTINPUTINFO plii);

        [DllImport("Kernel32.dll")]
        private static extern uint GetLastError();

        /// <summary>
        /// Retrieves the amount of time that has elapsed since the last user input.
        /// </summary>
        /// <returns>The idle time in milliseconds.</returns>
        public static uint GetIdleTime()
        {
            LASTINPUTINFO lastInPut = new LASTINPUTINFO();
            lastInPut.cbSize = (uint)Marshal.SizeOf(lastInPut);
            GetLastInputInfo(ref lastInPut);

            return ((uint)Environment.TickCount - lastInPut.dwTime);
        }

        /// <summary>
        /// Retrieves the time of the last input event.
        /// </summary>
        /// <returns>The time of the last input event.</returns>
        public static long GetLastInputTime()
        {
            // Create an instance of LASTINPUTINFO struct
            LASTINPUTINFO lastInPut = new LASTINPUTINFO();
            lastInPut.cbSize = (uint)Marshal.SizeOf(lastInPut);

            // Call GetLastInputInfo to get the last input information
            if (!GetLastInputInfo(ref lastInPut))
            {
                // Throw an exception if GetLastInputInfo fails
                throw new Exception(GetLastError().ToString());
            }

            // Return the time (milliseconds) of the last input event
            return lastInPut.dwTime;
        }

        /// <summary>
        /// Check if the system is idle.
        /// </summary>
        /// <returns>True if the system is idle, otherwise false.</returns>
        public static bool IsIdle()
        {
            return GetLastInputTime() != 0;
        }
    }
}
