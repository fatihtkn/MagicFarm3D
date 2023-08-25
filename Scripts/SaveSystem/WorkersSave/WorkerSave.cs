using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class WorkerSave 
{

    private const string workerSub = "worker.work";
    private const string workerCountSub = "workerCount.work";
    public static void Save(  List<WorkerRegister> List)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = /*Application.persistentDataPath + tile_Sub*/Path.Combine(Application.persistentDataPath, workerSub);
        string countPath = /*Application.persistentDataPath + tileCount_Sub*/Path.Combine(Application.persistentDataPath, workerCountSub);

        using (FileStream countStream = new FileStream(countPath, FileMode.Create))
        {
            formatter.Serialize(countStream, List.Count);
            countStream.Close();

        }


        for (int i = 0; i < List.Count; i++)
        {
            using (FileStream stream = new FileStream(path + i, FileMode.Create))
            {
                WorkerData data = new WorkerData(List[i]);
                formatter.Serialize(stream, data);
                stream.Close();

            }


        }
    }
}
