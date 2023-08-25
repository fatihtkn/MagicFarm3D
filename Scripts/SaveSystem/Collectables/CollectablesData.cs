using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class CollectablesData 
{

	public int crystalCount;
	public int leafCount;
	public int orbCount;
	public int potionCount;
	public int starCount;

	public CollectablesData(CollectablesManager collectables)
	{
		crystalCount = collectables.Crystal;
		leafCount = collectables.LeafCount;
		orbCount = collectables.OrbCount;
		potionCount = collectables.PotionCount;
		starCount = collectables.StarCount;
	}
}
