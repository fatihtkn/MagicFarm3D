using UnityEngine;
public class Register : MonoBehaviour
{
    public bool isObjectSold;
    [SerializeField] private GameObject buyArea;
    [SerializeField] private GameObject tile;
    private void Awake()
    {
        SaveSystem.tiles.Add(this);
    }
    private void Start()
    {
        if (isObjectSold)
        {
            buyArea.SetActive(false);

            if (tile != null)
            {
                tile.SetActive(true);
            }
               

        }
    }

    public void OnBoughtObject(bool firstBuy)
    {
        isObjectSold = true;
        buyArea.SetActive(false);

        
         tile.SetActive(true);

         tile.GetComponent<IAnimateable>().Animate(firstBuy);
        
        
        
        
        this.enabled = false;
    }
}
