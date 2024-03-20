
# Input Idle Handler
A simple tools to check if user is idle or not.

## How it works?
P/Invoke (Platform Invocation Services) is utilized to invoke functions from native Windows libraries directly within managed C# code. Specifically, it's used to call the GetLastInputInfo and GetLastError functions from the User32.dll and Kernel32.dll libraries, respectively.

#### Structure Definition:
- The `LASTINPUTINFO` structure is defined to store information about the last input event received by the system. It contains two members:
  - `cbSize`: Represents the size of the structure in bytes.
  - `dwTime`: Holds the time of the last input event, measured in milliseconds.

#### P/Invoke Declarations:
- Two functions from the Windows API are imported using Platform Invocation Services (P/Invoke):
  - `GetLastInputInfo`: Retrieves information about the last input event.
  - `GetLastError`: Retrieves the last-error code value for the calling thread.

#### GetIdleTime Method:
- This method retrieves the amount of time that has elapsed since the last user input event.
- It creates an instance of the `LASTINPUTINFO` struct, initializes its `cbSize`, and calls `GetLastInputInfo` to populate the struct with the last input information.
- The method calculates the idle time by subtracting the time of the last input event from the current system time (`Environment.TickCount`) and returns the result in milliseconds.

#### GetLastInputTime Method:
- This method retrieves the time (in milliseconds) of the last input event.
- It follows a similar process as the `GetIdleTime` method, creating an instance of `LASTINPUTINFO`, initializing its `cbSize`, and calling `GetLastInputInfo` to populate the struct.
- If `GetLastInputInfo` fails, it throws an exception, providing the error code returned by `GetLastError`.
- Finally, it returns the time of the last input event stored in the `dwTime` member of the `LASTINPUTINFO` struct.

#### IsIdle Method:
- This method checks if the system is currently idle by calling the `GetLastInputTime` method.
- If the time of the last input event is not zero, it indicates that the system is not idle, and the method returns `false`.
- If the time is zero, it signifies that the system is idle, and the method returns `true`.

#### Usage:
- You can use the `IdleHandler` class to monitor user activity and implement features like auto-locking screens after a period of inactivity, tracking user session duration, or triggering background tasks based on user idle time.


## How to use
You can just the function like this:
```
// Set the idle threshold to 5 seconds
const int idleThreshold = 5 * 1000;

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
```