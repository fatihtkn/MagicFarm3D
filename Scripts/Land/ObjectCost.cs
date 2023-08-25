using UnityEngine;
using TMPro;
using System;

public  class ObjectCost : MonoBehaviour
{
	public TMP_Text text;
	private float cost;
    public float TileCost
	{
		get { return cost; }

		set 
		{
			cost = value;
			
			SetCostUI();
        }
	}
	
	private void Awake()
	{
		
        text = GetComponent<TMP_Text>();
        if (cost <= 0) cost = Convert.ToInt32(text.text);

        SaveSystem.objectCost.Add(this);
		
	}
	

	private void SetCostUI()
	{
        if (cost >= 1000000) text.SetText((cost / 1000000).ToString("0.0") + "M");
        else if (cost >= 1000) text.SetText((cost / 1000).ToString("0.0") + "K");
        else text.SetText(cost.ToString("0"));
    }
	

}
