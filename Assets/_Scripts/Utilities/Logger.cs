using System;
using UnityEngine;

public class Logger
{
    static bool canLog = true;
    public static void Log(object message, UnityEngine.Object context)
    {
        if (canLog)
        {
            Debug.Log(message, context);
        }
    }
    public static void Log(object message)
    {
        if (canLog)
        {
            Debug.Log(message);
        }
    }

    public static void LogError(object message)
    {
        if (canLog)
        {
            Debug.Log(message);
        }
    }

    public static void LogError(object message, UnityEngine.Object context)
    {
        if (canLog)
        {
            Debug.Log(message, context);
        }
    }


    public static void LogWarning(object message, UnityEngine.Object context)
    {
        if (canLog)
        {
            Debug.Log(message, context);
        }
    }

    public static void LogWarning(object message)
    {
        if (canLog)
        {
            Debug.Log(message);
        }
    }
}