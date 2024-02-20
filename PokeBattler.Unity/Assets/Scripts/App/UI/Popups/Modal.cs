using PokeBattler.Client.Services;
using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using Zenject;

namespace PokeBattler.Unity
{
    public class ModalBehaviour : MonoBehaviour
    {
        [SerializeField]
        bool hasBackdrop = false;
        public bool HasBackdrop { get; }
        public Action<ModalBehaviour> OnClose { get; set; }
        public virtual void Close()
        {
            OnClose?.Invoke(this);
            Destroy(gameObject);
        }
    }

    public class ModalBehaviour<TOpenData> : ModalBehaviour
    {
        public virtual void Initialize(TOpenData data) { }
    }
}
