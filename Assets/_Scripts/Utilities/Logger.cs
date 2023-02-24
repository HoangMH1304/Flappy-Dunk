using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Logger
{
    public static void Log(string log)
    {
        if(BitPlayManager.Instance.showTestLog)
        {
            Debug.Log(log);
        }
    }

    public static void LogWarning(string log)
    {
        if(BitPlayManager.Instance.showTestLog)
        {
            Debug.LogWarning(log);
        }
    }

    public static void LogError(string log)
    {
        if(BitPlayManager.Instance.showTestLog)
        {
            Debug.LogError(log);
        }
    }
}
