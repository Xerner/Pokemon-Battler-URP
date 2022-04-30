using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DebugContent : MonoBehaviour
{
    public TextMeshProUGUI Header;
    public TextMeshProUGUI Content;

    public void UpdateContent(string content) => Content.text = content;
}
