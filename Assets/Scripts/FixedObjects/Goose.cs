using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goose : MonoBehaviour, IInteractable
{
    [SerializeField] SuccessMapManager _success;

    public void Interact()
    {
        _success.LaunchSuccessAnim(PlayerPrefsData.GOOSE_SUCCESS);
    }
}
