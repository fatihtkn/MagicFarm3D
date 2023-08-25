using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class CollectablesSave 
{
    private const string collectablesPath = "collectables.col";
    private const string CollectablesPathCount = "collectables.count";
    public static void Save(List<CollectablesManager> List)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = /*Application.persistentDataPath + tile_Sub*/Path.Combine(Application.persistentDataPath, collectablesPath);
        string countPath = /*Application.persistentDataPath + tileCount_Sub*/Path.Combine(Application.persistentDataPath, CollectablesPathCount);

        using (FileStream countStream = new FileStream(countPath, FileMode.Create))
        {
            formatter.Serialize(countStream, List.Count);
            countStream.Close();

        }


        for (int i = 0; i < List.Count; i++)
        {
            using (FileStream stream = new FileStream(path + i, FileMode.Create))
            {
                CollectablesData data = new CollectablesData(List[i]);
                formatter.Serialize(stream, data);
                stream.Close();

            }


        }
    }

}
