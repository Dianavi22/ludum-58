using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goose : MonoBehaviour, IInteractable
{
    [SerializeField] SuccessMapManager _success;
    [SerializeField] ParticleSystem _particle;

    public void Interact()
    {
        _particle.Play();
        _success.LaunchSuccessAnim(PlayerPrefsData.GOOSE_SUCCESS);
    }
}
