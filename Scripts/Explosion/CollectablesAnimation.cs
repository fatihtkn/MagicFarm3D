using System.Collections;
using UnityEngine;
using DG.Tweening;
using System.Collections.Generic;
public class CollectablesAnimation : MonoBehaviour
{
    public Transform targetTransform;
    public List<Transform> resetPosition;
    public float upForce=1f;
    public float sideForce=.1f;
    public List<GameObject> sphereList;
    public Ease easeType;
    public float completeTime;
    public float completeDelay=1f;
    //public enum ObjectType {Orb,Default,Gold,Potion,Leaf,Star };
    //public ObjectType objectType;
    
    
   
    private void OnEnable()
    {  
         
        StartCoroutine(CreateOrb());
        
    }
    public IEnumerator CreateOrb()
    {
        for (int i = 0; i < sphereList.Count; i++)
        {
            sphereList[i].SetActive(true);
            
            SphereExplosion(sphereList[i]);
            yield return new WaitForSeconds(0.07f);
        }
       
    }
    private void SphereExplosion(GameObject sphere)
    {
        float xForce = Random.Range(sideForce-2, sideForce);
        float yForce = Random.Range(upForce / 2, upForce);
        float zForce = Random.Range(sideForce-2, sideForce);

        Vector3 force = new Vector3(xForce, yForce, zForce);
        sphere.GetComponent<Rigidbody>().velocity = force;
        StartCoroutine(ResetSphere(sphere));
    }
    IEnumerator ResetSphere(GameObject sphere)
    {
        yield return new WaitForSeconds(completeDelay);
        sphere.transform.DOMove(targetTransform.position, completeTime).SetEase(easeType).OnComplete(() =>
        {
            //switch (objectType)
            //{
            //    case ObjectType.Orb:
            //        CollectablesManager.Instance.OrbCount += 1;
            //        break;
            //}
            ResetPositions(sphere);
        });
        

    }
    private void ResetPositions(GameObject collectableObject)
    {
       
        collectableObject.transform.localPosition = Vector3.zero;
        collectableObject.transform.rotation= Quaternion.identity;  
        collectableObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
        collectableObject.SetActive(false);
    }

   
}
