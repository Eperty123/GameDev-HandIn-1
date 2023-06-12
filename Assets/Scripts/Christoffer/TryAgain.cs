using UnityEngine;
using UnityEngine.SceneManagement;

public class TryAgain : MonoBehaviour
{
    public void StartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("TreasureScene-1", LoadSceneMode.Single);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
