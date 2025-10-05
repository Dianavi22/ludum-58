using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigRedButton : MonoBehaviour, IInteractable
{
    [SerializeField] SuccessMapManager _success;

    public void Interact()
    {
        _success.LaunchSuccessAnim(PlayerPrefsData.BUTTON_SUCCESS);
    }
}
