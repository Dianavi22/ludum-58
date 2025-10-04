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
    [SerializeField] GameObject _pauseMenu;
    void Start()
    {
        Resume();
    }

    void Update()
    {

        if ((Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P)) && !SuccessMapManager.isFading)
        {
            if (IsPause) { Resume(); } else { PausePlz(); }
        }
    }

    private void PausePlz()
    {
        IsPause = true;
        _pauseMenu.SetActive(true);
        Time.timeScale = 0;
    }

    public void Resume()
    {
        IsPause = false;
        _pauseMenu.SetActive(false);
        Time.timeScale = 1;
    }

    public void MainMenu()
    {
        if (SuccessMapManager.isFading) { return; }
        SceneManager.LoadScene(0);
    }
}
