using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool IsPause
    {
        get; private set;
    }

    public static bool IsMainMenu
    {
        get; private set;
    }

    public static bool IsInTheEnd;
    
    [SerializeField] GameObject _pauseMenu;
    [SerializeField] GameObject _mainMenu;
    [SerializeField] PausePanelController _ppc;
    [SerializeField] SuccessMapManager _succesMapManager;
    void Start()
    {
        Resume();
        IsMainMenu = false;
    }

    void Update()
    {

        if ((Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P)) && !SuccessMapManager.IsFading && !IsMainMenu && !IsInTheEnd)
        {
            if (IsPause) { Resume(); } else { PausePlz(); }
        }
    }

    private void PausePlz()
    {
        IsPause = true;
        _pauseMenu.SetActive(true);
        _succesMapManager.LaunchSuccessAnim(PlayerPrefsData.PAUSE_MENU);
    }

    public void Resume()
    {
        IsPause = false;
        _pauseMenu.SetActive(false);
        if (_ppc.Volume == 69)
        {
            _succesMapManager.LaunchSuccessAnim(PlayerPrefsData.MAGIC_NUMBER);
        }
    }

    public void MainMenu()
    {
        if (SuccessMapManager.IsFading) { return; }
        Resume();
        IsMainMenu = true;
        _mainMenu.SetActive(true);


    }
}
