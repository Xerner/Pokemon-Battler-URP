using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Menu))]
public class DebugPanelManager : MonoBehaviour
{
    static RectTransform StaticPanel;
    public DebugContent DebugPrefab;
    public static DebugContent StaticDebugPrefab;
    public static Dictionary<string, DebugContent> DebugContents = new Dictionary<string, DebugContent>();

    void Start() {
        StaticPanel = gameObject.GetComponent<RectTransform>();
        StaticDebugPrefab = DebugPrefab;
    }

    public static DebugContent Spawn(string header, string content) {
        DebugContent instance = Instantiate(StaticDebugPrefab, StaticPanel);
        instance.Header.text = header;
        instance.Content.text = content;
        DebugContents.Add(header, instance);
        return instance;
    }

    public static void Destroy(string header) => Destroy(DebugContents[header]);
}
