using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace KevinCastejon.EditorToolbox
{
    /// <summary>
    /// Bind triggers message methods to UnityEvents.
    /// </summary>
    [RequireComponent(typeof(Collider))]
    public class TriggerNotifier : MonoBehaviour
    {
        // Settings
        [SerializeField]
        private UnityEvent<Collider> _onEnter = new UnityEvent<Collider>();     // Evenement déclenché lors de l'entrée d'un collider
        [SerializeField]
        private UnityEvent<Collider> _onStay = new UnityEvent<Collider>();      // Evenement déclenché à chaque frame pour chaque collider entré
        [SerializeField]
        private UnityEvent<Collider> _onExit = new UnityEvent<Collider>();      // Evenement déclenché lors de la sortie d'un collider
        [SerializeField]
        private List<Collider> _colliders;                                      // Liste des colliders entrés
        [SerializeField]
        private Material _activeMaterial;                                       // Material quand au moins un collider est entré
        [SerializeField]
        private Material _inactiveMaterial;                                     // Material quand aucun collider n'est entré

        // Properties
        public int TriggeringCollidersCount { get => isActiveAndEnabled ? _colliders.Count : 0; }
        public List<Collider> TriggeringColliders { get => isActiveAndEnabled ? new List<Collider>(_colliders) : new List<Collider>(); }
        public UnityEvent<Collider> OnEnter { get => _onEnter; }
        public UnityEvent<Collider> OnStay { get => _onStay; }
        public UnityEvent<Collider> OnExit { get => _onExit; }
        public Material ActiveMaterial { get => _activeMaterial; set => _activeMaterial = value; }
        public Material InactiveMaterial { get => _inactiveMaterial; set => _inactiveMaterial = value; }

        private Renderer _renderer;

        private void Awake()
        {
            _renderer = GetComponent<Renderer>();
        }

        private void OnEnable()
        {
            // Pour chaque objets dans la liste
            for (int i = 0; i < _colliders.Count; i++)
            {
                // On déclenche l'evenement enter
                _onEnter.Invoke(_colliders[i]);
            }
        }

        private void OnDisable()
        {
            // Pour chaque objets dans la liste
            for (int i = 0; i < _colliders.Count; i++)
            {
                // On déclenche l'evenement exit
                _onExit.Invoke(_colliders[i]);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            // On ajoute l'objet à la liste
            _colliders.Add(other);

            // On déclenche l'evement
            _onEnter.Invoke(other);

            // Si c'est le premier objet à entrer dans la liste et qu'il ya un renderer
            if (_colliders.Count == 1 && _renderer && _activeMaterial)
            {
                // On change la material
                _renderer.material = _activeMaterial;
            }
        }

        private void OnTriggerStay(Collider other)
        {
            // On déclenche l'évenement
            _onStay.Invoke(other);
        }

        private void OnTriggerExit(Collider other)
        {
            // Si l'objet sortant n'est pas dans la liste on sort immédiatement de la méthode (si un objet est entré en collision alors que le GameObject était desactivé par exemple)
            if (!_colliders.Contains(other))
            {
                return;
            }

            // On retire l'objet de la liste
            _colliders.Remove(other);

            // On déclenche l'évenement
            _onExit.Invoke(other);

            // Si il n'y a plus d'objet dans la liste et qu'il ya un renderer
            if (_colliders.Count == 0 && _renderer && _inactiveMaterial)
            {
                // On change la material
                _renderer.material = _inactiveMaterial;
            }
        }

        // Retourne true si l'objet est dans la liste, sinon false
        public bool HasTriggeringCollider(Collider target)
        {
            return _colliders.Contains(target);
        }

        // Retourne l'index de l'objet s'il est présent dans la liste, sinon retourne -1
        public int IndexOfTriggeringCollider(Collider collider)
        {
            return _colliders.IndexOf(collider);
        }

        // Retourne l'objet présent dans la liste à l'index spécifié
        public Collider GetTriggeringColliderAt(int i)
        {
            return _colliders[i];
        }
    }
}
