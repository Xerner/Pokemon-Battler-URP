using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnapTo : MonoBehaviour
{
    public GameObject SavedObject;

    public void Reset() {
        SavedObject = null;
    }

    public void SetPokemon(GameObject gameObject) {
        SavedObject = gameObject;
        gameObject.transform.SetParent(transform);
        Snap();
    }

    /// <summary>Snaps the current held Pokemon back into place</summary>
    public void Snap() {
        if (SavedObject.transform is RectTransform) {
            ((RectTransform)SavedObject.transform).anchoredPosition = Vector2.zero;
            return;
        }
        SavedObject.transform.localPosition = Vector3.zero;
    }
}
