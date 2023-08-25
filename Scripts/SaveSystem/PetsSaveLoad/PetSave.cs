using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public  class PetSave
{
    private const string petPath = "pet.pet";
    private const string PetPathCount = "petCount.count";
    public static void Save(List<PetRegister> List)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = /*Application.persistentDataPath + tile_Sub*/Path.Combine(Application.persistentDataPath, petPath);
        string countPath = /*Application.persistentDataPath + tileCount_Sub*/Path.Combine(Application.persistentDataPath, PetPathCount);

        using (FileStream countStream = new FileStream(countPath, FileMode.Create))
        {
            formatter.Serialize(countStream, List.Count);
            countStream.Close();

        }


        for (int i = 0; i < List.Count; i++)
        {
            using (FileStream stream = new FileStream(path + i, FileMode.Create))
            {
                PetData data = new PetData(List[i]);
                formatter.Serialize(stream, data);
                stream.Close();

            }


        }
    }


}
    

