using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRegister : MonoSingleton<PlayerRegister>
{
    public Vector3 playerPos;
    public float CurrentMoney { get { return currentMoney; } set { currentMoney = value; if (currentMoney <= 0) currentMoney = 0; } }
    public float currentMoney;
   
    private void Awake()
    {

        SaveSystem.playerRegister.Add(this);
    }
    private void Start()
    {
        if (currentMoney > 0) { Money.Instance.CurrentMoney = currentMoney; }


        if (playerPos!= Vector3.zero) transform.position = playerPos;
        else
        {
            playerPos = transform.position;
        }
        
    }
    private void Update()
    {
        
    }

}
