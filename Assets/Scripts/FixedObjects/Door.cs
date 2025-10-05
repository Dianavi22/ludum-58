using UnityEngine;

public class Door : MonoBehaviour, IInteractable
{
    [SerializeField] private SuccessMapManager _success;

    private float _knockCounter;

    public void Interact()
    {
        if (SuccessMapManager.IsFading || PauseMenu.IsPause) { return; }

        _knockCounter++;
        
        if (1 == _knockCounter)
        {
            _success.LaunchSuccessAnim(PlayerPrefsData.KNOCK_SUCCESS);

        }

        if (13 == _knockCounter)
        {
            _success.LaunchSuccessAnim(PlayerPrefsData.KNOCK_KNOCK_KNOCK);
        }
    }
}
