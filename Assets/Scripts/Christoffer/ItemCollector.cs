using UnityEngine;

public class ItemCollector : MonoBehaviour
{
    TreasureManagerCD treasureManager;

    private void Start()
    {
        treasureManager = TreasureManagerCD.Instance;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Coin"))
        {
            Destroy(other.gameObject);
            treasureManager.AddTreasureCollected(1);
            treasureManager!.SetScore(treasureManager.GetCurrentCollectedTreasures(), treasureManager.MaxTreasures);
        }
    }
}
