using System.Collections;
using System.Collections.Generic;
using Rewards.Utils;
using UnityEngine;

[RequireComponent(typeof(Interacter))]
[RequireComponent(typeof(EntityMovement2D))]
public class MovementAchievements : MonoBehaviour
{   
    /// <summary>
    /// Reference to a movement script to track jumps.
    /// </summary>
    private EntityMovement2D _movement;

    /// <summary>
    /// Reference to interacter script to check interactions.
    /// </summary>
    private Interacter _interacter;
    [SerializeField] SuccessMapManager _successMapManager;
    private int _jumpCounter;
    private int _knockCounter;

    private void Awake()
    {
        _movement = GetComponent<EntityMovement2D>();
        _interacter = GetComponent<Interacter>();
        
        _movement.JumpEvent.AddListener(OnJump);
        _interacter.InteractEvent.AddListener(OnInteraction);
    }

    private void OnDestroy()
    {
        _movement.JumpEvent.RemoveListener(OnJump);
        _interacter.InteractEvent.RemoveListener(OnInteraction);
    }

    private void OnJump()
    {
        _jumpCounter++;

        if (50 == _jumpCounter)
        {
            PlayerPrefsUtils.SetBool(PlayerPrefsData.JUMPING_SUCCESS, true);
            _successMapManager.GetAllSuccessState();
        }
    }

    private void OnInteraction(IInteractable interactable)
    {
        if (interactable is Door)
        {
            _knockCounter++;
        }

        if (13 == _knockCounter)
        {
            _successMapManager.LaunchSuccessAnim(PlayerPrefsData.KNOCK_KNOCK_KNOCK);

          
        }

        if (1 == _knockCounter)
        {
            _successMapManager.LaunchSuccessAnim(PlayerPrefsData.KNOCK_SUCCESS);

        }
    }
}
