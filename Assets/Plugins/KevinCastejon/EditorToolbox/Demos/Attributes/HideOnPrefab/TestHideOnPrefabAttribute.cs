using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KevinCastejon.EditorToolbox;

public class TestHideOnPrefabAttribute : MonoBehaviour
{
    [HideOnPrefab]
    [SerializeField] private int _healthPoints;

    [HideOnPrefab(true)]
    [SerializeField] private int _damages;
}
