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

        if ((Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P)) && !SuccessMapManager.isFading && !IsMainMenu)
        {
            if (IsPause) { Resume(); } else { PausePlz(); }
        }
    }

    private void PausePlz()
    {
        IsPause = true;
        _pauseMenu.SetActive(true);
        Time.timeScale = 0;
        _succesMapManager.LaunchSuccessAnim(PlayerPrefsData.PAUSE_MENU);

    }

    public void Resume()
    {
        IsPause = false;
        _pauseMenu.SetActive(false);
        Time.timeScale = 1;
        if (_ppc.Volume == 69)
        {
            _succesMapManager.LaunchSuccessAnim(PlayerPrefsData.MAGIC_NUMBER);
        }
    }

    public void MainMenu()
    {
        if (SuccessMapManager.isFading) { return; }
        Resume();
        IsMainMenu = true;
        _mainMenu.SetActive(true);


    }
}
