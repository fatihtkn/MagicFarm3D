using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class WorkerData 
{


    public bool isWorkerSpawned;
    public int workerLevel;
    public float workerSpeedPercent;
    public int requireTile;
    public float upgradeCost;
    public float currentCollectSpeed;

    public WorkerData(WorkerRegister worker)
	{
        isWorkerSpawned=worker.isWorkerSpawned;
        workerLevel=worker.workerLevel; 
        workerSpeedPercent=worker.workerSpeedPercent;
        requireTile=worker.requireTile; 
        upgradeCost=worker.upgradeCost; 
        currentCollectSpeed=worker.currentCollectSpeed; 
	}
}
