using PokeBattler.Client.Services;
using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

/// <summary>
/// Manages a list of Behaviours or its own children who are assumed to represent in-game views
/// </summary>
namespace PokeBattler.Unity
{
    [AddComponentMenu("Poke Battler/View Manager")]
    public class ViewManagerBehaviour : MonoBehaviour
    {
        public bool AddChildrenAsViews;
        public bool ShowFirstViewOnStart;
        public List<GameObject> Views;

        IViewManagerService viewManager;

        [Inject]
        public void Construct(IViewManagerService viewManager)
        {
            this.viewManager = viewManager;
            viewManager.GameObject = gameObject;
        }

        void Start()
        {
            viewManager.Name = name;
            viewManager.AddViews(Views);
            if (AddChildrenAsViews)
            {
                viewManager.AddChildrenToViews(transform);
            }
            if (ShowFirstViewOnStart)
            {
                viewManager.TurnOffAllExceptFirst();
                return;
            }
            viewManager.TurnOffAll();
        }

        public void ChangeViews(GameObject view)
        {
            viewManager.ChangeViews(view);
        }

        public void Back()
        {
            viewManager.Back();
        }
    }
}
