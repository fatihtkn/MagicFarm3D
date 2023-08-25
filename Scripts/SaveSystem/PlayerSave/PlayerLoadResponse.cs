using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class PlayerLoadResponse 
{
    

    public  void Load(string playerSub,string playerCountSub,List<PlayerRegister> list)
    {
        
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Path.Combine(Application.persistentDataPath, playerSub) /*Application.persistentDataPath + tile_Sub*/ /*+ SceneManager.GetActiveScene().buildIndex*/;
        string countPath = Path.Combine(Application.persistentDataPath, playerCountSub);
        int tilecount = 0;
        if (File.Exists(countPath))
        {
            using (FileStream countStream = new FileStream(countPath, FileMode.Open))
            {
                tilecount = (int)formatter.Deserialize(countStream);
                countStream.Close();
            }

        }
        else
        {
            return;
        }
        //else return;

        for (int i = 0; i < tilecount; i++)
        {
            if (File.Exists(path + i))
            {

                using (FileStream stream = new FileStream(path + i, FileMode.Open))
                {
                    PlayerData data = formatter.Deserialize(stream) as PlayerData;
                    stream.Close();
                    list[i].CurrentMoney = data.currentMoney;
                    list[i].playerPos = new Vector3(data.playerPos[0], data.playerPos[1], data.playerPos[2]);
                }

               

            }
            else
            {
                Debug.LogError("path doesnt found" + path + i);
            }

        }

    }

}

