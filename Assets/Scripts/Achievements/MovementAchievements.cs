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

    [SerializeField] SuccessMapManager _successMapManager;
    private int _jumpCounter;

    private void Awake()
    {
        _movement = GetComponent<EntityMovement2D>();
        
        _movement.JumpEvent.AddListener(OnJump);
    }

    private void OnDestroy()
    {
        _movement.JumpEvent.RemoveListener(OnJump);
    }

    private void OnJump()
    {
        _jumpCounter++;

        if (70 == _jumpCounter)
        {
            PlayerPrefsUtils.SetBool(PlayerPrefsData.JUMPING_SUCCESS, true);
            _successMapManager.GetAllSuccessState();
        }
    }
}
