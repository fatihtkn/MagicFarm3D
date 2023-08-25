using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class CostLoad 
{
   public static void Load(string tileSub, string tileCountsub, List<ObjectCost> List)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Path.Combine(Application.persistentDataPath, tileSub) /*Application.persistentDataPath + tile_Sub*/ /*+ SceneManager.GetActiveScene().buildIndex*/;
        string countPath = Path.Combine(Application.persistentDataPath, tileCountsub);
        int tilecount = 0;
        if (File.Exists(countPath))
        {
            using (FileStream countStream = new FileStream(countPath, FileMode.Open))
            {
                tilecount = (int)formatter.Deserialize(countStream);
                countStream.Close();
            }

        }
        else return;

        for (int i = 0; i < tilecount; i++)
        {
            if (File.Exists(path + i))
            {

                using (FileStream stream = new FileStream(path + i, FileMode.Open))
                {
                    CostData data = formatter.Deserialize(stream) as CostData;
                    stream.Close();
                    List[i].TileCost = data.cost;

                }

                //Tile tile = Instantiate(tilePrefab,position,Quaternion.identity);

            }
            else return;

        }

    }
}
