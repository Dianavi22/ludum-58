using UnityEngine;

public class Chest : MonoBehaviour, IInteractable
{
    [SerializeField] SuccessMapManager _success;

	public void Interact()
    {
        print("AAAAAAAAA");
        _success.LaunchSuccessAnim(PlayerPrefsData.CHEST_SUCCESS);
    }
}
