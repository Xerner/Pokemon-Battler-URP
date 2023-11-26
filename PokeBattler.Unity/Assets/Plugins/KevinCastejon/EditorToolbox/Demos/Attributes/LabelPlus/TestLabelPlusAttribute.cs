using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KevinCastejon.EditorToolbox;

public class TestLabelPlusAttribute : MonoBehaviour
{
    [LabelPlus("Assets/KevinCastejon/EditorToolbox/Demos/Attributes/LabelPlus/Icons/greencrossicon.png", "Health Points", (int)LabelPlusColor.green)]
    [SerializeField] private int _healthPoints;
    [LabelPlus("Assets/KevinCastejon/EditorToolbox/Demos/Attributes/LabelPlus/Icons/atk.png", "Damages", new float[] { 1f, 0f, 0f, 1f })]
    [SerializeField] private int _damages;
}
