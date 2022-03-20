using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KevinCastejon.EditorToolbox;

public class TestReadOnlyAttribute: MonoBehaviour
{
    [ReadOnly]
    [SerializeField] private int _healthPoints;
    [ReadOnly]
    [SerializeField] private int _damages;
}
