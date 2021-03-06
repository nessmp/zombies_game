﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;

    public GameObject pauseMenuUI;
    public GameObject ControlsUI;

    private bool ControlsIsShowing = false;

    private void Start() 
    {
        GameIsPaused = false;

        pauseMenuUI.SetActive(false);
        ControlsUI.SetActive(false);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                if (ControlsIsShowing) 
                {
                    CloseControls();
                }
                else
                {
                    Resume();
                }
            }
            else
            {
                Pause();
            }
        }
        
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void OpenControls()
    {
        ControlsUI.SetActive(true);
        ControlsIsShowing = true;
    }

    public void CloseControls()
    {
        ControlsUI.SetActive(false);
        ControlsIsShowing = false;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}   
