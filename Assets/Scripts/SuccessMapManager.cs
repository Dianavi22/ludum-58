using System.Collections.Generic;
using UnityEngine;

public class SuccessMapManager : MonoBehaviour
{
    [SerializeField] List<Success> _success = new List<Success>();

    private void Start()
    {
        Test();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Test();
        }
    }

    private void Test()
    {
        print("AAAAA");
        for (int i = 0; i < _success.Count; i++)
        {
            if (_success[i].canBeSowed)
            {
                _success[i].ShowAllSuccess();
            }
        }
    }
}
