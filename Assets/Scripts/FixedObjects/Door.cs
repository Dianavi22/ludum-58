using UnityEngine;

public class Door : MonoBehaviour, IInteractable
{
    public void Interact()
    {
        Debug.LogWarning("This door does not open from this side!");
    }
}
