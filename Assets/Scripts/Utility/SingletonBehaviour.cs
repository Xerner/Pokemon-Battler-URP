using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Singleton behaviours can only have 1 class alive in the entire program.
/// Any classes created after the 1st are destroyed using Object.Destroy()
/// </summary>
public class SingletonBehaviour<T> : MonoBehaviour
{
    public static T Instance;
    public static T Singleton;
    private static SingletonBehaviour<T> instance;

    protected void Awake()
    {
        if (instance == null) {
            instance = this;
            Instance = GetComponent<T>();
            Singleton = GetComponent<T>();
        } else {
            Destroy(this);
        }
    }

    protected void Start() {
        if (instance == null) {
            throw new System.Exception("For some reason, a SingletonBehavior failed to instantiate its singleton!");
        } else {
            if (instance != this) Destroy(this);
        }
    }
}
