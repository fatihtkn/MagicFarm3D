using TMPro;
using UnityEngine;


public class SummonerTemplate : MonoBehaviour
{
    [Header("Summoner")]
    public GameObject spawnButton;
    public GameObject upgradeButton;
    public GameObject requireTileObject;

    public TMP_Text maxLevelText;
    public TMP_Text requireTileText;
    public TMP_Text spawnCostText;
    public TMP_Text summonerLevelText;
    public TMP_Text upgradeCostText;
    public TMP_Text currentTileCount;
    public TMP_Text speedText;

    public WorkerBrain currentWorker;

    public float speedIncreaseRate;
    public float upgradeCostIncreaseRate;
    public int requireTileIncreaseRate;

    
    public int maxLevel;
    public bool isWorkerSpawned;

    public WorkerRegister workerRegister;

    [SerializeField] private int requireTile;
    public int RequireTile
    {
        get { return requireTile; }
        set { requireTile = value; SetRequireTileText(value);  }
    }


   [SerializeField] private float upgradeCost;

    public float UpgradeCost
    {
        get { return upgradeCost; }
        set { upgradeCost = value; SetUpgradeCost(value); }
    }


   [SerializeField] private float spawnCost;

    public float SpawnCost
    {
        get { return spawnCost; }
        set { spawnCost = value; }
    }


   [SerializeField] private float collectSpeedPercent;

    public float CollectSpeedPercent
    {
        get { return collectSpeedPercent; }
        set 
        {   collectSpeedPercent = value;

            SetCollectSpeedText((value)); 
        }
    }

    private void SetRequireTileText(int value)
    {
        requireTile = value;
        requireTileText.SetText(requireTile);
    }

    private void SetUpgradeCost(float value)
    {
        upgradeCost=value;
        upgradeCostText.SetText(upgradeCost);
    }

    private void SetCollectSpeedText(float value)
    {

        collectSpeedPercent = value;
        speedText.SetText((collectSpeedPercent*10).ToString("0"));

    }
    [SerializeField]private int level;

    public int Level
    {
        get { return level; }
        set { level = value;SetLevelText(value); }
    }

    private void SetLevelText(int value)
    {
        summonerLevelText.SetText(value);  
    }



    public void SaveSpeedAndLevel()
    {
        workerRegister.workerLevel = level;
        workerRegister.workerSpeedPercent = collectSpeedPercent;
        workerRegister.requireTile = requireTile;
        workerRegister.upgradeCost = upgradeCost;
        workerRegister.currentCollectSpeed = currentWorker.collectSpeed;
        

    }
    public void SaveSpawn()
    {
        workerRegister.isWorkerSpawned = true;
      
    }
}
