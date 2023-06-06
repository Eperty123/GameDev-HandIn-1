using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Start : MonoBehaviour
{
    public void StartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("TreasureScene-1", LoadSceneMode.Single);
    }
}
