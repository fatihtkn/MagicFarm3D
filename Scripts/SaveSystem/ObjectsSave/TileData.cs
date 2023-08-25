using UnityEngine;
using System;

[Serializable]

public class TileData 
{
    public bool active;
    public float[] playerPos;
    public float cost;

    
    public TileData(Register register)
    {
        active = register.isObjectSold;
       


    }
    public TileData(BuyArea register)
    {
        cost = register.Price;



    }



}
