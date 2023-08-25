using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class CostSave 
{
   public static void  Save(string tileSub, string tileCountsub, List<ObjectCost> List)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = /*Application.persistentDataPath + tile_Sub*/Path.Combine(Application.persistentDataPath, tileSub);
        string countPath = /*Application.persistentDataPath + tileCount_Sub*/Path.Combine(Application.persistentDataPath, tileCountsub);

        using (FileStream countStream = new FileStream(countPath, FileMode.Create))
        {
            formatter.Serialize(countStream, List.Count);
            countStream.Close();

        }


        for (int i = 0; i < List.Count; i++)
        {
            using (FileStream stream = new FileStream(path + i, FileMode.Create))
            {
                CostData data = new CostData(List[i]);
                formatter.Serialize(stream, data);
                stream.Close();

            }


        }
    }
}
