using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;


public class SaveSystem : MonoBehaviour
{   
    private PlayerSaveResponse playerSave= new PlayerSaveResponse();
    private PlayerLoadResponse playerLoad= new PlayerLoadResponse();
      
    public static List<ObjectCost> objectCost=new List<ObjectCost>();
    
    public static List<Register> tiles= new List<Register>();
    public static List<PlayerRegister> playerRegister= new List<PlayerRegister>();
    public static List<PetRegister> petRegister = new List<PetRegister>();
   public static List<WorkerRegister> workerRegister= new List<WorkerRegister>();
    public static List<CollectablesManager> collectablesReegister = new List<CollectablesManager>();
   


    const string tile_Sub = "tile.til";
    const string tileCount_Sub = "tile.count";
    const string playerSub = "player.db";
    const string playerCountSub = "playerCount.db";
    const string Cost_Sub = "cost.c";
    const string CostCount_Sub = "cost.count";


    
    private void Awake()
    {

        playerLoad.Load(playerSub, playerCountSub, playerRegister);
        LoadTile();
        CostLoad.Load(Cost_Sub, CostCount_Sub, objectCost);
        WorkerLoad.Load(workerRegister);
        PetLoad.Load(petRegister);
        CollectablesLoad.Load(collectablesReegister);
    }
    private void Start()
    {
        
      
       
    }
    private void OnApplicationQuit()
    {
        CostSave.Save(Cost_Sub, CostCount_Sub, objectCost);
        SaveTile();
        playerSave.Save(playerSub, playerCountSub, playerRegister);
        WorkerSave.Save(workerRegister);
        PetSave.Save(petRegister);
        CollectablesSave.Save(collectablesReegister);
    }
    //private void OnApplicationPause(bool pause)
    //{

    //    if (pause)
    //    {
    //        CostSave.Save(Cost_Sub, CostCount_Sub, objectCost);
    //        SaveTile();
    //        playerSave.Save(playerSub, playerCountSub, playerRegister);
            
    //    }
        
        
    //}
    private void OnApplicationFocus(bool focus)
    {
        if (focus)
        {
            
        }
        else
        {
            CostSave.Save(Cost_Sub, CostCount_Sub, objectCost);
            SaveTile();
            playerSave.Save(playerSub, playerCountSub, playerRegister);
            WorkerSave.Save(workerRegister);
            PetSave.Save(petRegister);
            CollectablesSave.Save(collectablesReegister);
        }
    }


    void SaveTile()
    {
        BinaryFormatter formatter= new BinaryFormatter();
        string path = /*Application.persistentDataPath + tile_Sub*/Path.Combine(Application.persistentDataPath,tile_Sub);
        string countPath = /*Application.persistentDataPath + tileCount_Sub*/Path.Combine(Application.persistentDataPath, tileCount_Sub);
        
        using (FileStream countStream = new FileStream(countPath, FileMode.Create))
        {
            
            formatter.Serialize(countStream, tiles.Count);
            countStream.Close();

        }


        for (int i = 0; i < tiles.Count; i++)
        {
            using (FileStream stream = new FileStream(path + i, FileMode.Create))
            {
                TileData data = new TileData(tiles[i]);
                formatter.Serialize(stream, data);
                stream.Close();
                
            }
                

        }
    }
    void LoadTile()
    {
        
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Path.Combine(Application.persistentDataPath, tile_Sub) /*Application.persistentDataPath + tile_Sub*/ /*+ SceneManager.GetActiveScene().buildIndex*/;
        string countPath = Path.Combine(Application.persistentDataPath, tileCount_Sub);
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
                    TileData data = formatter.Deserialize(stream) as TileData;
                    stream.Close();
                    tiles[i].isObjectSold = data.active;
                  
                    //if(buyAreaSave[i]!=null) buyAreaSave[i].Price = data.cost;


                }

                //Tile tile = Instantiate(tilePrefab,position,Quaternion.identity);

            }
            else return;
            
        }

        

    }

    IEnumerator Timer()
    {
        yield return new WaitForSeconds(2f);
    }
   
    
}
