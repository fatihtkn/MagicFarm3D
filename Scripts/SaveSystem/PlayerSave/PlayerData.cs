using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public float currentMoney;
    public float[] playerPos;

    public PlayerData(PlayerRegister player)
    {
        currentMoney = player.CurrentMoney;
        Vector3 playerPosition = player.transform.position;
        playerPos = new float[]
        {
            playerPosition.x,playerPosition.y,playerPosition.z
        };
    }

    
}
