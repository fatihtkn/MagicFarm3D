using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkerRegister : MonoBehaviour
{

    public bool isWorkerSpawned;
    public int workerLevel;
    public float workerSpeedPercent;
    public int requireTile;
    public float upgradeCost;
    public float currentCollectSpeed;
    public SummonerTemplate workerTemplate;
    private void Awake()
    {

       SaveSystem.workerRegister.Add(this);




    }
    public void SAve()
    {
        SaveSystem.workerRegister.Add(this);
      
    }

    private void Start()
    {

        workerTemplate.currentWorker.gameObject.SetActive(isWorkerSpawned);
        if (isWorkerSpawned)
        {
            
            
            workerTemplate.CollectSpeedPercent = workerSpeedPercent;
            workerTemplate.currentWorker.collectSpeed = currentCollectSpeed;
            workerTemplate.Level = workerLevel;
            workerTemplate.RequireTile = requireTile;
            workerTemplate.isWorkerSpawned = isWorkerSpawned;
            workerTemplate.UpgradeCost = upgradeCost;
            workerTemplate.upgradeCostText.SetText(upgradeCost);

        }
       
      

    }

}
