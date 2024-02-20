using PokeBattler.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace PokeBattler.Client.Services
{
    public interface IModalService
    {
        public void Initialize(Transform backdrop, Transform modalParent, List<ModalBehaviour> modalPrefabs);
        public TModal Create<TModal, TOpenData>(TOpenData data) where TModal : ModalBehaviour<TOpenData>;
        public bool NeedsBackDrop();
        public bool BackDropIsActive();
        public void TurnOnBackdrop();
        public void TurnOffBackdrop();
    }

    public class ModalService : IModalService
    {
        public Transform ModalParent;
        public Transform Backdrop;
        readonly List<ModalBehaviour> openModals = new();
        public List<ModalBehaviour> ModalPrefabs = new();

        public void Initialize(Transform backdrop, Transform modalParent, List<ModalBehaviour> modalPrefabs)
        {
            ModalPrefabs = modalPrefabs;
            Backdrop = backdrop;
            ModalParent = modalParent;
            TurnOffBackdrop();
        }

        public void ValidateModalPrefabs()
        {
            foreach (var prefab_i in ModalPrefabs)
            {
                var type_i = prefab_i.GetType();
                foreach (var prefab_j in ModalPrefabs)
                {
                    var type_j = prefab_j.GetType();
                    if (type_i == type_j)
                    {
                        throw new InvalidOperationException($"Duplicate modal prefabs of type {type_i}");
                    }
                }
            }
        }

        public bool NeedsBackDrop()
        {
            
            return openModals.Exists(m => m.HasBackdrop);
        }

        public bool BackDropIsActive()
        {
            return Backdrop.gameObject.activeSelf;
        }

        public void TurnOnBackdrop()
        {
            Backdrop.gameObject.SetActive(true);
        }

        public void TurnOffBackdrop()
        {
            Backdrop.gameObject.SetActive(false);
        }

        public TModal GetModalPrefab<TModal, TOpenData>() where TModal : ModalBehaviour<TOpenData>
        {
            var prefab = ModalPrefabs.FirstOrDefault(prefab => prefab.TryGetComponent(out TModal modal));
            if (prefab == null)
            {
                throw new NullReferenceException($"Modal prefab not found for type {typeof(TModal)}");
            }
            var modal = prefab.GetComponent<TModal>();
            return modal;
        }

        public TModal Create<TModal, TOpenData>(TOpenData data) where TModal : ModalBehaviour<TOpenData>
        {
            var modalPrefab = GetModalPrefab<TModal, TOpenData> ();
            var modal = UnityEngine.Object.Instantiate(modalPrefab, ModalParent).GetComponent<ModalBehaviour<TOpenData>>();
            openModals.Add(modal);
            modal.Initialize(data);
            modal.OnClose = OnModalClosed;
            return (TModal)modal;
        }

        void OnModalClosed(ModalBehaviour modal)
        {
            openModals.Remove(modal);
            if (NeedsBackDrop() && !BackDropIsActive())
            {
                TurnOnBackdrop();
                return;
            }
            TurnOffBackdrop();
        }
    }
}