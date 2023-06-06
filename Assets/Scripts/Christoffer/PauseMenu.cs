using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;

    public GameObject pauseMenuUI;

    public GameObject ThirdPersonCameraCD;

    public GameObject Camera;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) 
        {
            if(GameIsPaused) 
            {   
                Resume();
            } else 
            {
                Pause();
            }
        }
    }
    
    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        ThirdPersonCameraCD.SetActive(true);
        Camera.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        ThirdPersonCameraCD.SetActive(false);
        Camera.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void Home() {
        ThirdPersonCameraCD.SetActive(false);
        Camera.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = false;
        SceneManager.LoadScene("Start", LoadSceneMode.Single);
    }
}
