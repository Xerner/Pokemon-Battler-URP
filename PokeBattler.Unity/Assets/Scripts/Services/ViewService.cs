using PokeBattler.Common.Extensions;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace PokeBattler.Client.Services
{
    public interface IViewManagerService
    {
        public string Name { get; set; }
        public GameObject GameObject { get; set; }
        void AddViews(IEnumerable<GameObject> views);
        void AddChildrenToViews(Transform transform);
        void TurnOffAll();
        void TurnOffAllExceptFirst();
        void ChangeViews(GameObject view);
        void Back();
    }

    public class ViewManagerService : IViewManagerService
    {
        public string Name { get; set; }
        public GameObject GameObject { get; set; }
        public List<GameObject> Views = new();
        public GameObject activeView;
        //[Header("The 0 index of the Views list, or the 1st child of this GameObject")]
        readonly Stack<GameObject> previousViews = new();
        readonly Serilog.ILogger logger;

        public ViewManagerService(Serilog.ILogger logger)
        {
            this.logger = logger;
        }

        public void AddViews(IEnumerable<GameObject> views)
        {
            Views.AddRange(views);
        }

        public void AddChildrenToViews(Transform transform)
        {
            transform.Select((child) => Views.Add(child.gameObject));
        }

        public void TurnOn(GameObject view)
        {
            if (view == null)
            {
                throw new NullViewException();
            }
            view.SetActive(true);
            activeView = view;
            logger.Information($"Turned on {view.name}");
        }

        public void TurnOff(GameObject view)
        {
            if (view == null)
            {
                throw new NullViewException();
            }
            view.SetActive(false);
            logger.Information($"Turned off {view.name}");
        }

        public void TurnOffAll()
        {
            foreach (var view in Views)
            {
                TurnOff(view);
            }
        }

        public void TurnOffAllExceptFirst()
        {
            TurnOffAll();
            if (Views.Count < 1)
            {
                return;
            }
            TurnOn(Views[0]);
        }

        public void ChangeViews(GameObject view)
        {

            // TODO: add case for when view is not in view list
            if (!Views.Contains(view))
            {
                throw new Exception($"{view.name} is not part of the {Name} View Manager's view list");
            }
            if (activeView == view)
            {
                return;
            }
            if (activeView != null)
            {
                TurnOff(activeView);
            }
            previousViews.Push(activeView);
            TurnOn(view);
        }

        public void Back()
        {
            if (previousViews.Count < 1)
            {
                return;
            }
            if (activeView != null)
            {
                TurnOff(activeView);
            }
            activeView = previousViews.Pop();
            TurnOn(activeView);
        }

        #region Exceptions

        public class NullViewException : Exception
        {
            public NullViewException() : base("A view listed in View Manager is null! Maybe a view was deleted?")
            {
            }
        }

        #endregion
    }
}