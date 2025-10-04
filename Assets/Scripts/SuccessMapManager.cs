using System.Collections.Generic;
using UnityEngine;

public class SuccessMapManager : MonoBehaviour
{
    [SerializeField] List<Success> _success = new List<Success>();
    [SerializeField] GameObject _theKey;
    [SerializeField] GameObject _theList;
    [SerializeField] GameObject _map;
    [SerializeField] GameObject _button;

    private void Start()
    {
        ShowStateSuccess();
    }

    private void Update()
    {

    }

    private void GetSuccess(string key)
    {

    }


    private void ShowStateSuccess()
    {
        CheckAllSuccess();
        for (int i = 0; i < _success.Count; i++)
        {
            if (_success[i].canBeShowed)
            {
                _success[i].ShowAllSuccess();
            }

            else if (_success[i].canBeOnlyImageView)
            {
                _success[i].ShowOnlySprite();
            }

            else if (_success[i].canBeOnlyTextView)
            {
                _success[i].ShowOnlyName();
            }
        }
    }

    private void CheckAllSuccess()
    {
        int id = 0;
        for (int i = 0; i < _success.Count; i++)
        {
            print(_success[i].canBeShowed + _success[i].name);

            if (_success[i].canBeShowed)
            {
                id++;
            }
        }
    
        if(id == _success.Count)
        {
            _theKey.SetActive(true);
            _theList.SetActive(false);
        }
       
    }

    public void ShowSuccessMap()
    {
        _map.SetActive(!_map.activeSelf);
    }
}
