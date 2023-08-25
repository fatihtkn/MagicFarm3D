using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.UI;

public class SummonerBuilder 
{

	private SummonerTemplate template;
	private bool spawnButtonControl;
	private bool upgradeButtonControl;
	
	private UnityAction spawnAction;
	private UnityAction upgradeAction;
	public SummonerBuilder(SummonerTemplate template)
	{
		
		this.template = template;
		spawnAction += SpawnWorkerButton;
		upgradeAction += UpGradeButton;
        spawnButtonControl =true;
		upgradeButtonControl =true;
		SetSpawnUpgradeStartCost();
		SetStartUp();
		InitalizeButtons(spawnAction, template.spawnButton.GetComponent<Button>());
        InitalizeButtons(upgradeAction, template.upgradeButton.GetComponent<Button>());
		if (template.Level >= 5) template.maxLevelText.enabled = true; Debug.Log("MaxLevel");
    }


	public void ControlRequireTile()
	{
	
		template.currentTileCount.SetText(PriortyManager.Instance.PriortyCount);
		SetMaxLevelText();
        if (template.RequireTile <= PriortyManager.Instance.PriortyCount)
		{
			

			SpawnController();
			UpgradeController();
		}
	}
    #region Spawn

	private void SpawnController()
	{
		if (spawnButtonControl&!template.isWorkerSpawned)
		{
            template.spawnButton.SetActive(true);
			template.SaveSpeedAndLevel();
        }
      
		
    }

    public void SpawnWorkerButton()
	{

        if (Money.Instance.CurrentMoney >= template.SpawnCost & spawnButtonControl)
        {
			template.SaveSpawn();
            template.spawnButton.SetActive(false);
            template.currentWorker.gameObject.SetActive(true);
            template.RequireTile += template.requireTileIncreaseRate;
            Money.Instance.CurrentMoney -= template.SpawnCost;
            spawnButtonControl = false;
           
			template.isWorkerSpawned=true;
        }

        
	}

	private void SetSpawnUpgradeStartCost()
	{
		template.spawnCostText.SetText(template.SpawnCost);
		template.upgradeCostText.SetText(template.UpgradeCost);
		

	}
	private void SetStartUp()
	{
		template.spawnButton.SetActive(false);
		template.upgradeButton.SetActive(false);
		template.Level = 1;
		template.requireTileText.SetText(template.RequireTile);
		
    }
    #endregion

    #region Upgrade
	private void UpgradeController() 
	{
		if (upgradeButtonControl&template.Level<5)
		{
            template.upgradeButton.SetActive(true);
        }
		
   

    }
	public void UpGradeButton()
	{
        if (Money.Instance.CurrentMoney >= template.UpgradeCost &template.isWorkerSpawned & template.RequireTile <= PriortyManager.Instance.PriortyCount)
        {
            
            template.RequireTile += template.requireTileIncreaseRate;
           
            template.UpgradeCost += template.upgradeCostIncreaseRate;
            Money.Instance.CurrentMoney -= template.UpgradeCost;
			MaxLevelControl();
			

        }



        

	}


	
	
    #endregion

	private void InitalizeButtons(UnityAction action,Button button)
	{
		button.onClick.AddListener(action);
	}

	private void MaxLevelControl()
	{
		if (template.Level < template.maxLevel)
		{
            template.Level += 1;
			//template.summonerLevelText.SetText(template.Level);
			
            
            if (template.Level >= template.maxLevel)
			{
                upgradeButtonControl = false;
                

				template.requireTileObject.SetActive(false);	
                template.upgradeButton.SetActive(false);
				template.maxLevelText.enabled=true;
				
            }

            SetCollectSpeed();
            
        }
		

	}

	private void SetCollectSpeed()
	{
		
		float rate= template.CollectSpeedPercent * 15*0.01f;
		
		template.CollectSpeedPercent += rate;
		template.currentWorker.collectSpeed -= rate;

        template.SaveSpeedAndLevel();

    }
	
	private void SetMaxLevelText()
	{
		if (template.Level >= 5)
		{
            template.maxLevelText.enabled = true;
			//template.requireTileText.SetText("Max!");
			//template.currentTileCount.SetText("Max!");
        }
		
	}
	
}
