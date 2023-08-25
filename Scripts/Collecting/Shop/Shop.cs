using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Shop : MonoBehaviour,IOnTaskCompleted
{
    [SerializeField] private List<GameObject> potions;
    [SerializeField] private Transform shopPos;
    [SerializeField] private float completeTime;
    [SerializeField] private Ease easeType;
    [SerializeField] private GameObject coins;
    [SerializeField] private GameObject InfoUI;
    [SerializeField] private BoxCollider boxCollider;
    public Transform goldTargetPos;

    private TaskBasedAnimation taskBasedAnimation;
    
    public float moneyMultiplier;
    private void Start()
    {
        potions = CollectablePool.Instance.potion;
        taskBasedAnimation = GetComponent<TaskBasedAnimation>();
    }
   
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (CollectablesManager.Instance.PotionCount > 0)
            {
                if (CollectablesManager.Instance.PotionCount > 10)
                {


                    StartCoroutine(CreatePotions(potions.Count));
                }
                else
                {
                    StartCoroutine(CreatePotions(CollectablesManager.Instance.PotionCount));

                }
            }
            else
            {
                StartCoroutine(SetInfoUI(true,0f));
            }

        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(SetInfoUI(false,0.5f));
            
        }
    }

    private IEnumerator CreatePotions(int loopCount)
    {
        boxCollider.enabled = false;
        //coins.SetActive(false);
        
        for (int i = 0; i < loopCount; i++)
        {
           
            potions[i].SetActive(true);
            PotionAnimation(potions[i]);
            yield return new WaitForSeconds(0.2f);
            
        }
        boxCollider.enabled = true;
        taskBasedAnimation.StartTask(goldTargetPos);
        //coins.SetActive(true);
       CollectablesManager.Instance.SellPotions(moneyMultiplier);
    }
    private void PotionAnimation(GameObject potion)
    {
        potion.transform.DOMove(shopPos.position, completeTime).SetEase(easeType).OnComplete(() =>
        {
            potion.transform.localPosition = Vector3.zero;
            potion.SetActive(false);
            
            
            //potions will turn into gold object


        });
        
    }
    private IEnumerator SetInfoUI(bool control,float duration)
    {
        yield return new WaitForSeconds(duration);
        InfoUI.SetActive(control);
    }

    public void OnAllTaskCompleted()
    {
        throw new System.NotImplementedException();
    }
}
