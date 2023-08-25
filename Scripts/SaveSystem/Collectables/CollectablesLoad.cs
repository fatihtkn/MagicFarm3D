using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class CollectablesLoad 
{
    private const string collectablesPath = "collectables.col";
    private const string CollectablesPathCount = "collectables.count";

    public static void Load(List<CollectablesManager> List)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Path.Combine(Application.persistentDataPath, collectablesPath) /*Application.persistentDataPath + tile_Sub*/ /*+ SceneManager.GetActiveScene().buildIndex*/;
        string countPath = Path.Combine(Application.persistentDataPath, CollectablesPathCount);
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
                    CollectablesData data = formatter.Deserialize(stream) as CollectablesData;
                    stream.Close();
                    List[i].OrbCount = data.orbCount;
                    List[i].LeafCount = data.leafCount;
                    List[i].PotionCount = data.potionCount;
                    List[i].Crystal = data.crystalCount;
                    List[i].StarCount= data.starCount;


                }



            }
            else return;

        }

    }


}
