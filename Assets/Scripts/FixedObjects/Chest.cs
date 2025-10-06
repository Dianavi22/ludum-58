using UnityEngine;

public class Chest : MonoBehaviour, IInteractable
{
    [SerializeField] SuccessMapManager _success;
    [SerializeField] ParticleSystem _successParticle;
   
    public void Interact()
    {
        print(_successParticle);
        _successParticle.Play();
        _success.LaunchSuccessAnim(PlayerPrefsData.CHEST_SUCCESS);
    }
}
