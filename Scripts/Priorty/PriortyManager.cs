
public class PriortyManager : MonoSingleton<PriortyManager> 
{
   
    private  int _priortyCount;
    public int PriortyCount { get { return _priortyCount; } set { _priortyCount = value; CollectablesManager.Instance.SetPriortyText(_priortyCount); } }

   
    
}
