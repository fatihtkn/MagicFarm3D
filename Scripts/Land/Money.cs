using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Threading;

public class Money :MonoSingleton<Money>
{
    public TMP_Text moneyText;
    private float currentMoney;
    public float coinMultiplier=1f;
    public float coinMultiplierRef=1f;
    
    public float CurrentMoney { get { return currentMoney; } 
        set {
            
            currentMoney = value;
            PlayerRegister.Instance.CurrentMoney= value;
            
            moneyText.SetText(currentMoney.ToString("0"));
            if(currentMoney<0) currentMoney = 0;

            SetMoneyUI();
        }
            
    }
   
    private void Start()
    {
        
        currentMoney = PlayerRegister.Instance.CurrentMoney;

        SetMoneyUI();
    }
    

    private void SetMoneyUI()
    {
        if (currentMoney >= 1000000) moneyText.SetText((currentMoney / 1000000).ToString("0.0") + "M");
        else if (currentMoney >= 1000) moneyText.SetText((currentMoney / 1000).ToString("0.0") + "K");
        else moneyText.SetText(currentMoney.ToString("0"));
    }
    
}
