using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PetRegister : MonoBehaviour
{
    public PetStats stats;
    
    [Header("Increments")]
    public float attributeIncrement;
    public float upgradeCostIncrement;
    [Header("Start Values")]
    public int level;
    public float upgradeCost;
    
    public bool isPetBought;
    public float currentAttribute;
    public float temporaryAttributeValue;
    public float defaultAttributeValue;
    public bool isPetSelected;

    public Vector3 petPosition;

    private void Awake()
    {
        SaveSystem.petRegister.Add(this);
       
        
    }
    private void Start()
    {
       
        if (isPetBought)
        {
            SyncDatas();
            
            stats.ControlSelectedPet();
            if (isPetSelected)
            {
                stats.SetReferenceAttributes();
                
                SetPetPos();
            }
           
        }
        


    }
    private void SyncDatas()
    {
        
        
        stats.isPetBought = isPetBought;
        stats.upgradeCost = upgradeCost;
        stats.level = level;
        stats.upgradeCostIncrement = upgradeCostIncrement;
        
        stats.temporaryAttributeValue = temporaryAttributeValue;
        stats.currentAttribute = currentAttribute;
        stats.defaultAttributeValue = defaultAttributeValue;
        stats.isPetSelected = isPetSelected;


    }
    

    public void SaveDatas()
    {
        isPetBought = stats.isPetBought;
        upgradeCost=stats.upgradeCost;
        level = stats.level;    
        upgradeCostIncrement = stats.upgradeCostIncrement;
        temporaryAttributeValue = stats.temporaryAttributeValue;
        currentAttribute = stats.currentAttribute;
        defaultAttributeValue = stats.defaultAttributeValue;
        isPetSelected=stats.isPetSelected;
    }
    private void SetPetPos()
    {
        
        stats.petObject.transform.position = petPosition;
        stats.petObject.GetComponent<PetTracking>().StartTrack(true);
       
    }
}
