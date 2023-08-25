using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class WorkerLoad 
{

    private const string workerSub = "worker.work";
    private const string workerCountSub= "workerCount.work";

    public static void Load( List<WorkerRegister> List)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Path.Combine(Application.persistentDataPath, workerSub) /*Application.persistentDataPath + tile_Sub*/ /*+ SceneManager.GetActiveScene().buildIndex*/;
        string countPath = Path.Combine(Application.persistentDataPath, workerCountSub);
        int objectCount = 0;
        if (File.Exists(countPath))
        {
            using (FileStream countStream = new FileStream(countPath, FileMode.Open))
            {
                objectCount = (int)formatter.Deserialize(countStream);
                countStream.Close();
            }

        }
        else return;

        for (int i = 0; i < objectCount; i++)
        {
            if (File.Exists(path + i))
            {
                using (FileStream stream = new FileStream(path + i, FileMode.Open))
                {
                    WorkerData data = formatter.Deserialize(stream) as WorkerData;
                    stream.Close();
                    
                    List[i].isWorkerSpawned = data.isWorkerSpawned;
                    List[i].workerLevel = data.workerLevel;
                    List[i].workerSpeedPercent = data.workerSpeedPercent;
                    List[i].requireTile=data.requireTile;
                    List[i].upgradeCost = data.upgradeCost;
                    List[i].currentCollectSpeed = data.currentCollectSpeed;

                }

                

            }
            else return;

        }

    }
}
