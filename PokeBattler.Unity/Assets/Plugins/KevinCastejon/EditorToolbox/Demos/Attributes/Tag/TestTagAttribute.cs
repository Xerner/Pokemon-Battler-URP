using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KevinCastejon.EditorToolbox;

public class TestTagAttribute : MonoBehaviour
{
    [Tag]
    [SerializeField] private string _targetTag;
}
