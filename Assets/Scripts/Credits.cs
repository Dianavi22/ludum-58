using Rewards.Utils;
using UnityEngine;

public class Credits : MonoBehaviour
{
    [SerializeField] SuccessMapManager _successMapManager;
    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            _successMapManager.LaunchSuccessAnim(PlayerPrefsData.HAS_RESPECTED_CREATORS);
        }
    }
}
