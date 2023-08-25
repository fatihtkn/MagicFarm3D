using System.Collections;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class PegasusManager : MonoBehaviour
{   
    
    public Transform startPos;
    public float multiplier;
    public BoxCollider boxCollider;
    public float horseLoopTime;
    public Transform targetPos;
    public int requiredOrbCount;
    public TMP_Text requiredOrbText;
    public GameObject requiredOrbImage;
    public Transform OrbBasketTarget;
    public Transform coinUITarget;

    private TaskBasedAnimation taskBasedAnimation;


    private void Start()
    {
        taskBasedAnimation= GetComponent<TaskBasedAnimation>(); 
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") & CollectablesManager.Instance.OrbCount >= requiredOrbCount)
        {
            
            StartCoroutine(SetInformationUI(false, 0f));
            StartCoroutine(HorseLoop());
        }
        if (other.CompareTag("Player") & CollectablesManager.Instance.OrbCount < requiredOrbCount)
        {
            StartCoroutine(SetInformationUI(true, 0f));
           
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(SetInformationUI(false, 0.5f));
           
        }
    }
    private IEnumerator HorseLoop()
    {   //Orb Anim
        boxCollider.enabled = false;
        CollectablePool.Instance.SelectPool(PoolType.Orb, OrbBasketTarget);

        

        yield return new WaitForSeconds(1.8f);

        taskBasedAnimation.StartTask(coinUITarget);

        CollectablesManager.Instance.SellOrbs(multiplier);
        
        transform.DOMove(targetPos.position, horseLoopTime).OnComplete(() =>
        {
            boxCollider.enabled = true;
            transform.position = startPos.position;
            
        });
    }
    private IEnumerator SetInformationUI(bool control,float duration)
    {
        yield return new WaitForSeconds(duration);
        requiredOrbImage.SetActive(control);
        requiredOrbText.SetText(CollectablesManager.Instance.OrbCount.ToString());



    }
}
