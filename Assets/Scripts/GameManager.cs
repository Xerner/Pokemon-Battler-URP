using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [SerializeField] private Arena[] arenas;
    [SerializeField] TrainerManager trainerManager;
    public static bool DebugMode;

    private void Start()
    {
        Instance = this;
        DebugMode = true;
    }

}
