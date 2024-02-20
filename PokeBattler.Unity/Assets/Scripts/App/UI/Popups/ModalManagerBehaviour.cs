using PokeBattler.Client.Services;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

namespace PokeBattler.Unity
{
    [AddComponentMenu("Poke Battler/UI/Modal Manager")]
    public class ModalManagerBehaviour : MonoBehaviour
    {
        [SerializeField]
        Transform backdrop;
        [SerializeField]
        List<ModalBehaviour> AvailableModalTypes = new();
        IModalService modalService;

        [Inject]
        public void Construct(IModalService modalService)
        {
            this.modalService = modalService;
        }

        void Start()
        {
            modalService.Initialize(backdrop, transform, AvailableModalTypes);
        }
    }
}