using UnityEngine;

public class Door : MonoBehaviour, IInteractable
{
    public void Interact()
    {
        if (SuccessMapManager.IsFading || PauseMenu.IsPause) { return; }
        Debug.LogWarning("This door does not open from this side!");
    }
}
