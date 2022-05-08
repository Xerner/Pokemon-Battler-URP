using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

// https://fistfullofshrimp.com/unity-drag-things-around/
public class DragMouseMove : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerMoveHandler {

    private Plane daggingPlane;
    private Vector3 offset;

    private Camera mainCamera;

    void Start() {
        mainCamera = Camera.main;
    }

    public void OnPointerDown(PointerEventData eventData) {
        daggingPlane = new Plane(mainCamera.transform.forward,
                              transform.position);
        Ray camRay = mainCamera.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(camRay.origin, camRay.direction * 10, Color.green);

        float planeDistance;
        daggingPlane.Raycast(camRay, out planeDistance);
        offset = transform.position - camRay.GetPoint(planeDistance);
    }

    public void OnPointerUp(PointerEventData eventData) {
        //throw new System.NotImplementedException();
    }

    public void OnPointerMove(PointerEventData eventData) {
        Ray camRay = mainCamera.ScreenPointToRay(Input.mousePosition);
        float planeDistance;
        daggingPlane.Raycast(camRay, out planeDistance);
        transform.position = camRay.GetPoint(planeDistance) + offset;
    }
}