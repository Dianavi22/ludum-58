using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigRedButton : MonoBehaviour, IInteractable
{
    [SerializeField] SuccessMapManager _success;
    [SerializeField] ParticleSystem _buttonPart;

    public void Interact()
    {
        _buttonPart.Play();
        _success.LaunchSuccessAnim(PlayerPrefsData.BUTTON_SUCCESS);
    }
}
