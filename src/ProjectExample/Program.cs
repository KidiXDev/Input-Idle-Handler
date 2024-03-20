using idh;
using System.Threading;
using System;

namespace ProjectExample
{
    public class Program
    {
        static void Main(string[] args)
        {
            // Set the idle threshold to 5 seconds
            const int idleThreshold = 5 * 1000;

            while (true)
            {
                //check if the user is idle and idle time exceeds the threshold
                if (IdleHandler.IsIdle() && IdleHandler.GetIdleTime() > idleThreshold)
                {
                    Console.WriteLine("User is idle");
                    // Do something
                }
                else
                {
                    Console.WriteLine("Active");
                    // Do something
                }

                // Wait for one second before checking again
                Thread.Sleep(1 * 1000);
            }
        }
    }
}
