using UnityEngine;

public class Door : MonoBehaviour, IInteractable
{
    public void Interact()
    {
        if (SuccessMapManager.isFading || PauseMenu.IsPause) { return; }
        Debug.LogWarning("This door does not open from this side!");
    }
}
