using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class PetSelection : MonoBehaviour
{
    

    public List<PetStats> pets;
    public AnimalShelter shelter;
    

    private int index=0;

   
    private void Start()
    {
        
    }
    public void NextPet()
    {
        
        index++;
        if(index>=pets.Count) index=0;
        GetPet(index);

    }
    public void BackButton()
    {
     
        
        index--;
        if(index<0) index=pets.Count-1;
        GetPet(index);
    }

    private void GetPet(int index)
    {

        for (int i = 0; i < pets.Count; i++)
        {
            if (i==index)
            {
                pets[i].gameObject.SetActive(true);
                pets[i].petUI.SetActive(true);
                pets[i].petButtons.SetActive(true);
            }
            else
            {
                pets[i].gameObject.SetActive(false);
                pets[i].petUI.SetActive(false);
                pets[i].petButtons.SetActive(false);
            }
        }

        

    }
    public void ClosePage()
    {
        shelter.cam.SetActive(false);  
        shelter.petSelectingPage.SetActive(false);
        CharacterMovement.Instance.moveInputPanel.SetActive(true);
        PlayerManager.Instance.SetCharacterMeshActivity(true);
        SetPetActivity();
        shelter.collectablesCanvas.SetActive(true);

        for (int i = 0; i < pets.Count; i++)
        {
            pets[i].gameObject.SetActive(false);
            pets[i].petObject.GetComponent<PetTracking>().StartTrack(pets[i].isPetSelected);
           
        }
        
    }

    public void DefaultPet(int i)
    {
        pets[i].gameObject.SetActive(true);
        pets[i].petUI.SetActive(true);
        pets[i].petButtons.SetActive(true);
    }

    private void SetPetActivity()
    {
        for (int i = 0; i < pets.Count; i++)
        {

            if (pets[i].isPetSelected)
            {
                pets[i].petObject.SetActive(true);
            }
            else
            {
                pets[i].petObject.SetActive(false);
            }

        }
    }
    public void DeactivePetObjects()
    {
        for (int i = 0; i < pets.Count; i++)
        {
            pets[i].petObject.SetActive(false);
        }
    }


}
