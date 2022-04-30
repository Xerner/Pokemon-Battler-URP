using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [SerializeField] private Arena[] arenas;
    public static bool DebugMode;

    private void Start()
    {
        Instance = this;
        DebugMode = true;
    }

}
