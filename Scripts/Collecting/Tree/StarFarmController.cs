using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;


public enum TreeStates
{
    Charged,
    Collecting,
    Charging
}

public class StarFarmController : MonoBehaviour,IInterractable
{
    UnityAction action;
    [SerializeField] Image treeImage;
    [Header("Speed")]
    float collectSpeed;
    [SerializeField] float chargeSpeed;
    [SerializeField] TaskBasedAnimation[] tasksBasedCollectables;
    public TreeStates currentState;

    Collider col;
    private void Awake()
    {
        col= GetComponent<Collider>();
        col.enabled = false;
    }
    private void Start()
    {
        
        action += Interract;
        StartCoroutine(ColliderDelay());
    }

    public void Interract()
    {
        bool chargeControl= currentState== TreeStates.Charged;
        if (chargeControl)
        {
            
            collectSpeed = PlayerManager.Instance.PlayerCollectSpeed;
            CharacterMovement.Instance.enabled = false;
            Collecting();
            StartCoroutine(AnimationCoolDown());
            
        }
    }
   
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ButtonManager.Instance.AddListener(ButtonTypes.Wand, action);
            if (currentState == TreeStates.Charged) ButtonManager.Instance.wandButton.SetActive(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ButtonManager.Instance.wandButton.SetActive(false);
            ButtonManager.Instance.RemoveListener(ButtonTypes.Wand, action);

        }
    }

    void Collecting()
    {
        currentState = TreeStates.Collecting;
        treeImage.DOFillAmount(0f, collectSpeed).OnComplete(() =>
        {
            CollectablesManager.Instance.StarCount += 10;
            Charging();
        });
    }
    void Charging()
    {
        currentState = TreeStates.Charging;
        treeImage.DOFillAmount(1f, collectSpeed).OnComplete(() =>
        {
            Charged();
        });
    }
    void Charged()
    {
        currentState = TreeStates.Charged;
        StarAnimsReset();
    }


    IEnumerator AnimationCoolDown()
    {
        PlayerManager.Instance.animController.SetConditions("WandHit",true);
        
        yield return new WaitForSeconds(1f);
        RippleTriggerControl.Instance.StartRipple();

        CharacterMovement.Instance.enabled = true;
        PlayerManager.Instance.animController.SetConditions("WandHit", false);
    }


    void StarAnimsReset()
    {
        for (int i = 0; i < tasksBasedCollectables.Length; i++)
        {
            tasksBasedCollectables[i].OnCollectableCharged();
        }
    }


    IEnumerator ColliderDelay()
    {
        yield return new WaitForSeconds(1.6f);
        col.enabled = true;

    }

}
