using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TreasureManagerCD : SingletonCD<TreasureManagerCD>
{
    public int MaxTreasures = 1;
    public string TreasureCollectedString = "Treasures collected:";
    public string GameWinSceneName = "GameWin";

    public GameObject TreasureScoreUIPrefab;
    public TextMeshProUGUI Text;

    int treasuresCollected;
    [SerializeField] GameObject treasureScoreUI;

    private void Start()
    {
        treasureScoreUI = Instantiate(TreasureScoreUIPrefab);
        Text = treasureScoreUI.GetComponentInChildren<TextMeshProUGUI>();
        //SetText(TreasureCollectedString);
    }

    private void Update()
    {
    }

    private void CheckTreasuresCollected()
    {
        if (treasuresCollected >= MaxTreasures)
            SceneManager.LoadScene(GameWinSceneName, LoadSceneMode.Single);
    }

    public void SetText(string text)
    {
        Text!.text = text;
    }

    public void SetScore(int score, int max)
    {
        SetText($"{TreasureCollectedString} {score}/{max}");
    }

    public void SetMaxTreasures(int max)
    {
        MaxTreasures = max;
    }

    public void SetCurrentTreasureCollected(int amount)
    {
        treasuresCollected += amount;
        CheckTreasuresCollected();
    }

    public int GetCurrentCollectedTreasures()
    {
        return treasuresCollected;
    }
}
