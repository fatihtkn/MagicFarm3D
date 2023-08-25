using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;


public class PetLoad 
{
    private const string PetPath = "pet.pet";
    private const string PetPathCount = "petCount.count";

    public static void Load(List<PetRegister> List)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Path.Combine(Application.persistentDataPath, PetPath) /*Application.persistentDataPath + tile_Sub*/ /*+ SceneManager.GetActiveScene().buildIndex*/;
        string countPath = Path.Combine(Application.persistentDataPath, PetPathCount);
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
                    PetData data = formatter.Deserialize(stream) as PetData;
                    stream.Close();
                   List[i].attributeIncrement = data.attributeIncrement;
                   List[i].upgradeCostIncrement = data.upgradeCostIncrement;
                   List[i].level = data.level;
                   List[i].upgradeCost = data.upgradeCost;
                   List[i].isPetBought = data.isPetBought;
                   List[i].currentAttribute = data.currentAttribute;
                   List[i].temporaryAttributeValue = data.temporaryAttributeValue;
                   List[i].defaultAttributeValue = data.defaultAttributeValue;
                    List[i].isPetSelected= data.isPetSelected;
                    List[i].petPosition = new Vector3(data.petPos[0], data.petPos[1], data.petPos[2]);

                }



            }
            else return;

        }

    }
}
