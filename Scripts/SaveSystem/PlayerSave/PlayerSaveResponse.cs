using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class PlayerSaveResponse : ISaveable<PlayerRegister>
{
    public void Save(string playerSub, string playerCountsub, List<PlayerRegister> playerList)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = /*Application.persistentDataPath + tile_Sub*/Path.Combine(Application.persistentDataPath, playerSub);
        string countPath = /*Application.persistentDataPath + tileCount_Sub*/Path.Combine(Application.persistentDataPath, playerCountsub);

        using (FileStream countStream = new FileStream(countPath, FileMode.Create))
        {
            formatter.Serialize(countStream, playerList.Count);
            countStream.Close();
        }
        for (int i = 0; i < playerList.Count; i++)
        {
            using (FileStream stream = new FileStream(path + i, FileMode.Create))
            {
                PlayerData data = new PlayerData(playerList[i]);
                formatter.Serialize(stream, data);
                stream.Close();

            }


        }
    }
}
