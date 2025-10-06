using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGame : MonoBehaviour
{

    [SerializeField] SuccessMapManager smm;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.attachedRigidbody.CompareTag("Player"))
        {
            smm.ShowTheEnd();
        }
    }
}
