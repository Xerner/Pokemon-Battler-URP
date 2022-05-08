using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public bool FreeRefreshShop = false;
    public bool FreeExperience = false;
    public bool FreePokemon = false;

    private void Start()
    {
        Instance = this;
    }
}
