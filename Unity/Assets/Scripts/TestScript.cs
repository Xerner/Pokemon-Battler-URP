using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TestScript : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public void OnPointerEnter(PointerEventData eventData) {
        Debug2.Log("Cursor entered bench"); // , LogLevel.All);
    }

    public void OnPointerExit(PointerEventData eventData) {
        Debug2.Log("Cursor exited bench"); // , LogLevel.All);
    }
}
