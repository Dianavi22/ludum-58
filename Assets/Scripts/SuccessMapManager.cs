using System.Collections.Generic;
using Rewards.Utils;
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
        GetAllSuccessState();
        ShowStateSuccess();
    }

    private void OnDestroy()
    {
        PlayerPrefsUtils.SetBool(PlayerPrefsData.HAS_QUIT_THE_GAME, true);

        GetAllSuccessState();
    }

    private void GetAllSuccessState()
    {
        for (int i = 0; i < _success.Count; i++)
        {
            _success[i].SuccessDatas.isSuccess = PlayerPrefsUtils.TryGetBool(_success[i].SuccessDatas.successKey);
        }

    }

    private void Update()
    {
        //Test :
        if (Input.GetKeyDown(KeyCode.I))
        {
            PlayerPrefsUtils.SetBool(PlayerPrefsData.HAS_RESPECTED_CREATORS, true);
            GetAllSuccessState();
        }
    }

    private void ShowStateSuccess()
    {
        CheckAllSuccess();
        for (int i = 0; i < _success.Count; i++)
        {
            if (_success[i].SuccessDatas.isSuccess)
            {
                _success[i].ShowAllSuccess();
            }

            else if (_success[i].SuccessDatas.showImage)
            {
                _success[i].ShowOnlySprite();
            }

            else if (_success[i].SuccessDatas.showImage)
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

            if (_success[i].SuccessDatas.isSuccess)
            {
                id++;
            }
        }

        if (id == _success.Count)
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
