using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectablePool : MonoSingleton<CollectablePool>  
{

    public Transform targetTransform;
    
    public List<Transform> resetPosition;  
    public float upForce = 1f;
    public float sideForce = .1f;
    private List<GameObject> objectList;
    public Ease easeType;
    public float completeTime;
    public enum ObjectType { Orb, Leaf, Potion };
    

    [Header("CollectablesPool")]
    [SerializeField] private List<GameObject> leaf;
    [SerializeField] public List<GameObject> potion;
    [SerializeField] private List<GameObject> orb;

    
    
    public void SelectPool(PoolType type,Transform targetPosition)
    {
        switch (type)
        {
            case PoolType.Orb:
                objectList = orb;
                break;
            case PoolType.Leaf:
                objectList = leaf;
                break;
            case PoolType.Potion:
                objectList = potion;
                break;
            
        }
        targetTransform = targetPosition;
        StartCoroutine(CreateOrb());
    }
    public IEnumerator CreateOrb()
    {
        for (int i = 0; i < objectList.Count; i++)
        {
            objectList[i].SetActive(true);
            ObjectAnimation(objectList[i]);
            yield return new WaitForSeconds(0.08f);
        }

    }
    private void ObjectAnimation(GameObject sphere)
    {
       
       
        sphere.transform.DOMove(targetTransform.position, completeTime).SetEase(easeType).OnComplete(() =>
        {
            ResetPositions(sphere);
        });
    }
    
    private void ResetPositions(GameObject collectableObject)
    {
        
        collectableObject.transform.localPosition = Vector3.zero;
        collectableObject.SetActive(false);
    }
    

}
