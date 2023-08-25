using TMPro;
using UnityEngine;

public class TileGenerator : MonoBehaviour
{   
    GameObject tile;
    GameObject buyArea;
    [Header("Objects")]
    
    [SerializeField]GameObject tileRef;
    [SerializeField] GameObject buyAreaUIRef;
    [SerializeField] GameObject LandNoUI;
    [SerializeField] GameObject lockedBridge;
    [SerializeField] GameObject selectedObject;
    [Header("Parent Objects")]
    [SerializeField] Transform buyAreaParentObject;
    [SerializeField] Transform tileParentObject;
    [SerializeField] Transform landNoParentObject;
    
    [Header("Positions")]
    public GameObject representativePosition;

    public Vector3 pos;
    [Header("Values")]
    [SerializeField] private float cost;
    public Vector3 RefPos { get { return pos; } 
        set
        { 
            RefPos = pos;
            print("sa");
            representativePosition.transform.position = pos;
        } 
    }


    public void AddValueX()
    {
        pos.x += 6.6f;
    }
    public void ReduceValueX()
    {
        pos.x  -=6.6f;
    }
    public void AddTileValueZ()
    {
        pos.z += 6.6f;
    }
    public void ReduceTileValueZ()
    {
        pos.z -= 6.6f;
    }
    public void GenerateTile()
    {

        tile=Instantiate(tileRef, pos, Quaternion.Euler(-90,0,0)) as GameObject;
        tile.transform.SetParent(tileParentObject, false);
    }
    
    public void GenerateBuyArea()
    {
        Vector3 areaPos = new Vector3(pos.x, pos.z,0 );
        buyArea=Instantiate(buyAreaUIRef,areaPos, Quaternion.identity);
        buyArea.transform.SetParent(buyAreaParentObject, false);
       
    }
    public void GenerateLockedBridge()
    {
       
        Vector3 areaPos = new Vector3(pos.x, pos.z, 0);
        GameObject bridge = Instantiate(lockedBridge, areaPos, Quaternion.identity) as GameObject;
        bridge.transform.SetParent(buyAreaParentObject, false);

    }
    public void SetObjectPosition()
    {
        
        selectedObject.transform.position = pos;
    }
    public void ResetPosition()
    {
        pos= Vector3.zero;  
    }
    public void SetLandNo()
    {
        Vector3 areaPos = new Vector3(pos.x, pos.z, -1.7f);
        GameObject landNo = Instantiate(LandNoUI, areaPos, Quaternion.identity);
        landNo.transform.SetParent(landNoParentObject, false);
    }
    public void SetTileCost()
    {
        selectedObject.GetComponentInChildren<TMP_Text>().SetText(cost.ToString());
    }
}
