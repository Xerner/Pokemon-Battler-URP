using UnityEngine;

public class Debug2 : Debug {
    public static LogLevel CurrentLogLevel = LogLevel.Detailed;

    public static void Log(object message, LogLevel logLevel = LogLevel.Normal, Object context = null) 
        { if (logLevel <= CurrentLogLevel) Debug.Log(constructMessage(message, logLevel), context); }
    
    public static void LogError(object message, LogLevel logLevel = LogLevel.Normal, Object context = null) 
        { if (logLevel <= CurrentLogLevel) Debug.LogError(constructMessage(message, logLevel), context); }
    
    public static void LogWarning(object message, LogLevel logLevel = LogLevel.Normal, Object context = null)
        { if (logLevel <= CurrentLogLevel) Debug.LogWarning(constructMessage(message, logLevel), context); }   

    private static string constructMessage(object message, LogLevel logLevel) {
        string color = "white";
        switch (logLevel) {
            case LogLevel.None:
                return "";
            case LogLevel.Normal:
                return message.ToString();
            case LogLevel.Detailed:
                color = "#90ee90";  // light green
                break;
            case LogLevel.All:
                color = "#9090ee"; // light blue
                break;
        }
        return $"<b><color={color}>{logLevel.ToString()}</color></b>\t{message.ToString()}";
    }
}

public enum LogLevel {
    None,
    Normal,
    Detailed,
    All
}
