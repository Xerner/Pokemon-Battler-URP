using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DebugContent : MonoBehaviour
{
    public TextMeshProUGUI Header;
    public GameObject Content;

    public void UpdateContent(GameObject content) => Content = content;
}
