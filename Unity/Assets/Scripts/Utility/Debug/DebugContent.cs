using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DebugContent : MonoBehaviour
{
    public TextMeshProUGUI Header;
    public UnityEngine.GameObject Content;

    public void UpdateContent(UnityEngine.GameObject content) => Content = content;
}
