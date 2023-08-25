using System;
using TMPro;
using UnityEngine;

public class PriotyControl : MonoBehaviour
{
	private int priortyCount;
	private int refCount;
	[SerializeField] private BuyArea buyArea;
	[SerializeField] private TMP_Text costText;
	

	
	private void Awake()
	{
		refCount =  Convert.ToInt32(gameObject.name);

	}
	private void Update()
	{
		

		priortyCount = PriortyManager.Instance.PriortyCount;

		if (refCount == priortyCount)
		{
			PriortyController();
		}
		
		
    }

	private void PriortyController()
	{

		costText.enabled = true;
		buyArea.gameObject.SetActive(true);
		this.enabled = false;
      
    }
	
}
