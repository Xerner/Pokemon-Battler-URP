using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

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

    public static DebugContent Spawn(string header, GameObject content) {
        DebugContent instance = Instantiate(StaticDebugPrefab, StaticPanel);
        instance.Header.text = header;
        instance.Content = content;
        DebugContents.Add(header, instance);
        return instance;
    }

    public static DebugContent Spawn(string header) {
        DebugContent instance = Instantiate(StaticDebugPrefab, StaticPanel);
        instance.Header.text = header;
        DebugContents.Add(header, instance);
        return instance;
    }

    public static void UpdateSize() {
        var csf = StaticPanel.gameObject.GetComponent<ContentSizeFitter>();
        csf.verticalFit = ContentSizeFitter.FitMode.Unconstrained;
        csf.verticalFit = ContentSizeFitter.FitMode.PreferredSize;
    }

    public static void Destroy(string header) => Destroy(DebugContents[header]);
}
