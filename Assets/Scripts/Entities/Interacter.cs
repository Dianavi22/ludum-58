using UnityEngine;
using UnityEngine.Events;

public class Interacter : MonoBehaviour
{
    /// <summary>
    /// The range of the circle cast that detects IInteractable objects.
    /// </summary>
    [SerializeField, Tooltip("The interaction range.")] private float _interactionRange;

    /// <summary>
    /// Layers that this object can interact with.
    /// </summary>
    [SerializeField, Tooltip("Layers this object can interact with.")] private LayerMask _canInteractWith;

    /// <summary>
    /// Whether the show the gizmos or not.
    /// </summary>
    [SerializeField, Tooltip("Display the gizmos for debug purpose.")] private bool _debugShowGizmos;

    /// <summary>
    /// Key that this interacter will react with.
    /// </summary>
    [SerializeField, Tooltip("Keycode that this interacter interacts with.")] private KeyCode _keycode;

    [SerializeField] Animator _animator;

    private PlayerSFXManager _sfxManager;

    private void Awake()
    {
        _sfxManager = GetComponentInChildren<PlayerSFXManager>();
    }

    private void Update()
    {
        // On interact: gets all interactable in range and interact with the closest one.
        if (Input.GetKeyDown(_keycode))
        {
            _sfxManager.PlayInteract();
            _animator.SetTrigger("interact");

            IInteractable nearestInteractable = null;
            Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, _interactionRange);
            float minDist = float.MaxValue;

            foreach (Collider2D hit in hits)
            {
                if (Tools.IsLayerWithinMask(hit.gameObject.layer, _canInteractWith) && hit.attachedRigidbody.TryGetComponent<IInteractable>(out IInteractable interactable))
                {
                    float distance = Vector2.Distance(transform.position, hit.transform.position);
                    if (distance < minDist)
                    {
                        minDist = distance;
                        nearestInteractable = interactable;
                    }
                }
            }

            nearestInteractable?.Interact();
        }
    }

    private void OnDrawGizmos()
    {
        if (!_debugShowGizmos)
        {
            return;
        }

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _interactionRange);
    }
}
