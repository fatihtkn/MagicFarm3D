

[System.Serializable]
public class CostData 
{
    public float cost;

	public CostData(ObjectCost costBase)
	{
		cost = costBase.TileCost;
	}
}
