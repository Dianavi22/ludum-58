using UnityEngine;

public class Chest : MonoBehaviour, IInteractable
{
    [SerializeField] SuccessMapManager _success;

    [SerializeField] ShakyCam _shakyCam;
	public void Interact()
    {
        _shakyCam.ShakyCameCustom(0.15f, 0.3f);
        _success.LaunchSuccessAnim(PlayerPrefsData.CHEST_SUCCESS);
    }
}
