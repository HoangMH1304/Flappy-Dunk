using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InheritanceSingleton<T> : UILobbyController where T : InheritanceSingleton<T>
{
    static T _instance;
    public static T Instance
    {
        get
        {
            if (_instance == null) _instance = (T)FindObjectOfType(typeof(T));
            if (_instance == null) Debug.LogError("An instance of " + typeof(T) + " is needed in the scene, but there is none.");
            return _instance;
        }
    }

    protected void Awake()
    {
        if (_instance == null) _instance = this as T;
        else if (_instance != this) Destroy(this);
    }
}
