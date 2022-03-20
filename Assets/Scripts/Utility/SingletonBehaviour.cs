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

    void Start()
    {
        if (Instance is null) {
            Instance = GetComponent<T>();
        } else {
            Destroy(this);
        }
    }
}
