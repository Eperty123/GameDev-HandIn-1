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
    Scene activeScene;

    public override void Awake()
    {
        base.Awake();
        SceneManager.activeSceneChanged += SceneManager_activeSceneChanged;
    }

    private void SceneManager_activeSceneChanged(Scene arg0, Scene arg1)
    {
        activeScene = arg1;
        CheckUI();
        ResetTreasureCollected();
    }

    private void CheckUI()
    {
        if (treasureScoreUI == null && TreasureScoreUIPrefab != null)
        {
            treasureScoreUI = Instantiate(TreasureScoreUIPrefab);
            Text = treasureScoreUI.GetComponentInChildren<TextMeshProUGUI>();
        }
    }

    private void Start()
    {
        CheckUI();
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

    public void AddTreasureCollected(int amount)
    {
        treasuresCollected += amount;
        CheckTreasuresCollected();
    }

    public void ResetTreasureCollected()
    {
        treasuresCollected = 0;
    }

    public int GetCurrentCollectedTreasures()
    {
        return treasuresCollected;
    }
}
