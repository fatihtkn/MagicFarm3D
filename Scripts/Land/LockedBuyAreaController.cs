using System;
using TMPro;
using UnityEngine;

public class LockedBuyAreaController : MonoBehaviour
{
    [SerializeField] private GameObject lockedBuyArea;
    [SerializeField] private GameObject buyArea;
    private TMP_Text lockedBuyText;
  
    private int buyAreaUnlockNumber;
    public int lockedAreaEnablePriorty;

    private Register register;
    private bool controller;
    private void Awake()
    {
        
    }
    private void Start()
    {
        lockedBuyText = lockedBuyArea.GetComponentInChildren<TMP_Text>();
        buyAreaUnlockNumber = Convert.ToInt32(buyArea.name);
        register=GetComponent<Register>();
    }
    private void Update()
    {
        if (PriortyManager.Instance.PriortyCount == lockedAreaEnablePriorty)
        {
            controller = true;

        }
        if (controller)
        {
            lockedBuyArea.SetActive(true);
            lockedBuyText.SetText((PriortyManager.Instance.PriortyCount + "/" + buyAreaUnlockNumber).ToString());
            

        }


        if (PriortyManager.Instance.PriortyCount / buyAreaUnlockNumber == 1 || register.isObjectSold)
        {
            lockedBuyArea.SetActive(false);
            //buyArea.SetActive(true);
            register.isObjectSold = true;
            this.enabled = false;
        }

    }
    

}
