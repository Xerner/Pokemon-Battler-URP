using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manages a list of GameObjects or its own children who are assumed to represent in-game views
/// </summary>
public class ViewManager : SingletonBehaviour<ViewManager>
{
    [Description("Assumes this GameObjects children are to be managed instead of the list below")]
    public bool UseChildren;
    public List<GameObject> Views;
    //[Header("The 0 index of the Views list, or the 1st child of this GameObject")]
    public static GameObject activeView;
    private static readonly Stack<GameObject> previousViews = new Stack<GameObject>();

    private void Start() {
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
        foreach (var view in Views) view.SetActive(false);
        Views[0].SetActive(true);
    }

    #endregion

    #region public methods

    public void ChangeViews(GameObject view) {

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
