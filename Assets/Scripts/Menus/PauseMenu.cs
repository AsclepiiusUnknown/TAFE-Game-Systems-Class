using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public static bool isPaused;
    public GameObject pauseMenu, optionsMenu;
    void Paused()
    {
        //Pause everything
        isPaused = true;
        //Stop Time
        Time.timeScale = 0;
        //open Pause Menu
        pauseMenu.SetActive(true);
        //Release the cursor
        Cursor.lockState = CursorLockMode.None;
        //show the cursor
        Cursor.visible = true;
    }
    public void UnPaused()
    {
        //UnPause everything
        isPaused = false;
        //Start Time
        Time.timeScale = 1;
        //Close Pause Menu
        pauseMenu.SetActive(false);
        //Lock the cursor
        Cursor.lockState = CursorLockMode.Locked;
        //Hide the cursor
        Cursor.visible = false;
    }
    private void Start()
    {
        UnPaused();
    }
    void TogglePause()
    {
        if (!isPaused)
        {
            Paused();
        }
        else
        {
            UnPaused();
        }
    }
    private void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            //if the Options Panel is not on
            if (!optionsMenu.activeSelf)
            {
                //Toggle Freely
                TogglePause();
            }
            else
            {
                //close the options panel
                pauseMenu.SetActive(true);
                optionsMenu.SetActive(false);
            }
        }
    }
}
