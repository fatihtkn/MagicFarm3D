using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public  class LoadTest<BaseType,DataType>:MonoBehaviour where DataType: class /*where DataType : ILoadable<BaseType, DataType>*/
{
    
    
    public static List<DataType> LoadGame(string obj_Sub,string ObjCount_Sub)
    {
        List<DataType> gameData=new List<DataType>(); 
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Path.Combine(Application.persistentDataPath, obj_Sub) /*Application.persistentDataPath + tile_Sub*/ /*+ SceneManager.GetActiveScene().buildIndex*/;
        string countPath = Path.Combine(Application.persistentDataPath, ObjCount_Sub);
        int tilecount = 0;
        if (File.Exists(countPath))
        {
            using (FileStream countStream = new FileStream(countPath, FileMode.Open))
            {
                tilecount = (int)formatter.Deserialize(countStream);
                countStream.Close();
            }

        }
        

        for (int i = 0; i < tilecount; i++)
        {
            if (File.Exists(path + i))
            {

                using (FileStream stream = new FileStream(path + i, FileMode.Open))
                {
                    DataType data = formatter.Deserialize(stream) as DataType;
                    stream.Close();
                    //objectList[i].isObjectSold = data.active;
                    gameData.Add(data);
                }

                //Tile tile = Instantiate(tilePrefab,position,Quaternion.identity);

            }
            else return null;

        }
        return gameData;
    }
   
    
}
