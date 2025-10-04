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
        for (int i = 0; i < _success.Count; i++)
        {
            if (_success[i].canBeShowed)
            {
                _success[i].ShowAllSuccess();
            }

            if (_success[i].canBeOnlyImageView)
            {
                _success[i].ShowOnlySprite();
            }

            if (_success[i].canBeOnlyTextView)
            {
                _success[i].ShowOnlyName();
            }
        }
    }
}
