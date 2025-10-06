using UnityEngine;

public class Chest : MonoBehaviour, IInteractable
{
    [SerializeField] SuccessMapManager _success;

	public void Interact()
    {
        _success.LaunchSuccessAnim(PlayerPrefsData.CHEST_SUCCESS);
    }
}
