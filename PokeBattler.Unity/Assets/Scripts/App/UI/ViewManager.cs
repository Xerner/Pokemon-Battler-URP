using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manages a list of Behaviours or its own children who are assumed to represent in-game views
/// </summary>
public class ViewManager : MonoBehaviour
{
    public static ViewManager Instance { get; private set; }

    [Description("Assumes this Behaviours children are to be managed instead of the list below")]
    public bool UseChildren;
    public List<UnityEngine.GameObject> Views;
    //[Header("The 0 index of the Views list, or the 1st child of this GameObject")]
    public static UnityEngine.GameObject activeView;
    private static readonly Stack<UnityEngine.GameObject> previousViews = new Stack<UnityEngine.GameObject>();

    void Awake() {
        if (Instance != null && Instance != this) {
            Destroy(this);
            return;
        } else {
            Instance = this;
        }
    }

    void Start() {
        if (UseChildren) {
            if (transform.childCount == 0) throw new System.Exception("View Manager is set to use its children, but it has none!");
            addChildrenToViews();
        } else {
            if (Views.Count == 0) throw new System.Exception("View Manager is set to use a list of views, but that has no items!");
            activeView = Views[0];
        }
        turnOffAllExceptFirst();
    }

    #region private methods

    private void addChildrenToViews() {
        for (int i = 0; i < transform.childCount; i++) {
            Views.Add(transform.GetChild(i).gameObject);
        }
    }

    private void turnOffAllExceptFirst() {
        foreach (var view in Views) {
            if (view == null) throw new System.Exception("A view listed in View Manager is null! Maybe a view was deleted?");
            view.SetActive(false);
        }
        Views[0].SetActive(true);
    }

    #endregion

    #region public methods

    public void ChangeViews(UnityEngine.GameObject view) {

        // TODO:  add case for when  view is not in view list
        if (!Views.Contains(view)) throw new System.Exception($"{view.name} is not part of the {gameObject.name} View Manager's view list");
        activeView?.SetActive(false);
        previousViews.Push(activeView);
        activeView = view;
        view.SetActive(true);
    }

    public void Back() {
        activeView?.SetActive(false);
        activeView = previousViews.Pop();
        activeView.SetActive(true);
    }

    #endregion
}
