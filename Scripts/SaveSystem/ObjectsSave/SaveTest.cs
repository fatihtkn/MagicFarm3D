using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SaveTest<BaseType> where BaseType:Register
{
    public static void Save(string pathSub,string pathCountSub,List<BaseType> list)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = /*Application.persistentDataPath + tile_Sub*/Path.Combine(Application.persistentDataPath, pathSub);
        string countPath = /*Application.persistentDataPath + tileCount_Sub*/Path.Combine(Application.persistentDataPath, pathCountSub);

        using (FileStream countStream = new FileStream(countPath, FileMode.Create))
        {
            formatter.Serialize(countStream, list.Count);
            countStream.Close();

        }


        for (int i = 0; i < list.Count; i++)
        {
            using (FileStream stream = new FileStream(path + i, FileMode.Create))
            {
                //TileData data = new TileData(list[i]);
                //formatter.Serialize(stream, data);
                //stream.Close();

            }


        }
    }
}
