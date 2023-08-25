using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PetData 
{
    public float attributeIncrement;
    public float upgradeCostIncrement;

    public int level = 1;
    public float upgradeCost;

    public bool isPetBought;
    public float currentAttribute;
    public float temporaryAttributeValue;
    public float defaultAttributeValue;
    public bool isPetSelected;


    public float[] petPos;
    public PetData(PetRegister pet)
	{
        attributeIncrement = pet.attributeIncrement;
        upgradeCostIncrement = pet.upgradeCostIncrement;
        level = pet.level;
        upgradeCost = pet.upgradeCost;
        isPetBought = pet.isPetBought;
        currentAttribute = pet.currentAttribute;
        temporaryAttributeValue = pet.temporaryAttributeValue;
        defaultAttributeValue = pet.defaultAttributeValue;
        isPetSelected = pet.isPetSelected;

        Vector3 petPosition = pet.stats.petObject.transform.position;
        petPos = new float[]
        {
            petPosition.x,petPosition.y,petPosition.z
        };
	}




}
