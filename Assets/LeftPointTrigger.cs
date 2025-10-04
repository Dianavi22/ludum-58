using System.Collections;
using System.Collections.Generic;
using Rewards.Utils;
using UnityEngine;

public class LeftPointTrigger : MonoBehaviour
{

    [SerializeField] SuccessMapManager _successMapManager;
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerPrefsUtils.SetBool(PlayerPrefsData.TO_THE_LEFT, true);
            _successMapManager.GetAllSuccessState();
        }
    }

}
