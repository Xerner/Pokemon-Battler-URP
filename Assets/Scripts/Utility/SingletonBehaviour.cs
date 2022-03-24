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

    protected void Start()
    {
        if (Instance == null) {
            Instance = GetComponent<T>();
            Singleton = GetComponent<T>();
        } else {
            Destroy(this);
        }
    }
}
