using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestTriggerNotifier : MonoBehaviour
{
    public void OnEnter(Collider collider)
    {
        Debug.Log(collider + " entered the cube");
    }

    public void OnStay(Collider collider)
    {
        Debug.Log(collider + " stays in the cube");
    }

    public void OnExit(Collider collider)
    {
        Debug.Log(collider + " leaved the cube");
    }
}
