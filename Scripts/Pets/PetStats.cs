using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PetStats : MonoBehaviour
{
    public GameObject petUI;
    public GameObject petButtons;
    public GameObject petObject;
    [HideInInspector] public bool isPetSelected;
    public GameObject selectButtonsGroup;
    public PetRegister register;
    [Header("Increments")]

    public float attributeIncrement;
    public float upgradeCostIncrement;
   

    [Header("Start Values")]
    public int level = 1;
    public float upgradeCost;
    public int petCost;
    public int maxLevel;
    public bool isPetBought;
    public float currentAttribute;
    public float temporaryAttributeValue;
    public float defaultAttributeValue;
   

    [Header("Texts")]
    public TMP_Text attributeText;
    public TMP_Text levelText;
    public TMP_Text upgradeCostText;
    public TMP_Text buyText;
    

    [Header("Buttons")]
    public GameObject buyButton;
    public GameObject upgradeButton;
    public GameObject selectPet;
    public GameObject deSelectPet;

    public Animals animal;

    private void Start()
    {
        SetPriceText();
        SetLevelText();
        SetReferenceAttributes();
        SetAttributeText();
        SetUpgradeCostText();
        //ControlSelectedPet();
        SelecDeselectButtonsActivity();
        MaxLevelController(); 
    }

    private void OnEnable()
    {
       

        if (isPetBought)
        {
            buyButton.SetActive(false);
            upgradeButton.SetActive(true);

        }
        else
        {
            buyButton.SetActive(true);
            upgradeButton.SetActive(false);
        }


        SelecDeselectButtonsActivity();
        MaxLevelController();

    }






    public void BuyPet()
    {
        if (CollectablesManager.Instance.Crystal >= petCost)
        {
            isPetBought = true;
            CollectablesManager.Instance.Crystal-=petCost;
            buyButton.SetActive(false);
            upgradeButton.SetActive(true);
            SetUpgradeCost();
            SelecDeselectButtonsActivity();
            register.SaveDatas();
        }
       

    }



    public void IncreasePlayerMoveSpeed()//Owl
    {
        if (Money.Instance.CurrentMoney >= upgradeCost)
        {
            float value = CharacterMovement.Instance.MoveSpeedRef * attributeIncrement / 100;
            temporaryAttributeValue = CharacterMovement.Instance.MoveSpeedRef + value;
            CharacterMovement.Instance.MoveSpeedRef = temporaryAttributeValue;
            
            SetAttributeText();
            SetUpgradeCost();
            LevelUp();
            MaxLevelController();
            register.SaveDatas();
        }


    }
    public void IncreasePlayerCollectSpeed()//Fox
    {
        if (Money.Instance.CurrentMoney >= upgradeCost)
        {
            float value =  PlayerManager.Instance.collectSpeedRef * attributeIncrement / 100;
            temporaryAttributeValue = PlayerManager.Instance.collectSpeedRef - value;
            PlayerManager.Instance.collectSpeedRef =temporaryAttributeValue;

            SetAttributeText();
            SetUpgradeCost();
            LevelUp();
            MaxLevelController();
            register.SaveDatas();
        }
    }

    public void IncreaseCoinMultiplier()//Rabbit
    {

        if (Money.Instance.CurrentMoney >= upgradeCost)
        {
            float value = Money.Instance.coinMultiplierRef * attributeIncrement / 100;
            temporaryAttributeValue = Money.Instance.coinMultiplierRef + value;
            Money.Instance.coinMultiplierRef = temporaryAttributeValue;
            SetAttributeText();
            SetUpgradeCost();
            LevelUp();
            MaxLevelController();
            register.SaveDatas();
        }


        

    }


    public void SelectPet()
    {
        isPetSelected = true;
        selectPet.SetActive(false);
        deSelectPet.SetActive(true);
        currentAttribute = temporaryAttributeValue;
        SetAttributeofAnimal();
        //petObject.GetComponent<PetTracking>().StartTrack(true);
        register.SaveDatas();

    }
    public void DeSelectPet()
    {
        isPetSelected = false;
        deSelectPet.SetActive(false);
        selectPet.SetActive(true);
        currentAttribute= defaultAttributeValue;
        SetAttributeofAnimal();
        //petObject.GetComponent<PetTracking>().StartTrack(false);
        register.SaveDatas();

    }

    private void SetUpgradeCost()
    {
        Money.Instance.CurrentMoney -= upgradeCost;
        upgradeCost += upgradeCostIncrement;
        SetUpgradeCostText();


    }
    private void LevelUp()
    {
        level++;
        SetLevelText();

    }
    private void SetLevelText()
    {
        levelText.SetText(level);
    }
    private void MaxLevelController()
    {
        if (level >= maxLevel)
        {
            upgradeButton.SetActive(false);

        }
    }
   
   private void SelecDeselectButtonsActivity()
    {
        if (isPetBought)
        {
            selectButtonsGroup.SetActive(true);
        }
        else
        {

            selectButtonsGroup.SetActive(false);

            
        }
       
   }
    public void ControlSelectedPet()
    {
        if (isPetSelected)
        {
            petObject.SetActive(true);
            SelectPet();
            print("Pet");
        }
    }
    public void SetReferenceAttributes()
    {
        switch (animal)
        {
            case Animals.Owl:
                CharacterMovement.Instance.MoveSpeedRef = temporaryAttributeValue;
                break;
            case Animals.Rabbit:
                Money.Instance.coinMultiplierRef = temporaryAttributeValue;
                break;
            case Animals.Fox:
                PlayerManager.Instance.collectSpeedRef=temporaryAttributeValue;
                break;
            default:
                break;
        }

    }

    private void SetAttributeText()
    {
        
        if (animal == Animals.Owl)
        {
            attributeText.SetText(temporaryAttributeValue.ToString("0"));
        }
        else
        {
            attributeText.SetText(temporaryAttributeValue.ToString("0.0"));
        }
    }
    private void SetUpgradeCostText()
    {
        upgradeCostText.SetText(upgradeCost);
    }
    private void SetAttributeofAnimal()
    {
        switch (animal)
        {
            case Animals.Owl:
                
                CharacterMovement.Instance.MoveSpeed = currentAttribute;
                break;
            case Animals.Rabbit:
                Money.Instance.coinMultiplier = currentAttribute;
                break;
            case Animals.Fox:
                PlayerManager.Instance.PlayerCollectSpeed=currentAttribute;

                break;
            default:
                break;
        }
    }

    private void SetPriceText()
    {
        buyText.SetText(petCost);
    }

}
