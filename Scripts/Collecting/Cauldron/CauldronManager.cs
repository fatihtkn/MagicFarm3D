using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class CauldronManager : MonoBehaviour,IInterractable,IWorkerInterract,IOnTaskCompleted
{
    
    [SerializeField] private List<Animator> dragonAnimators;
    [SerializeField] private Image potionImage;
    [SerializeField] private float chargeSpeed;
    [SerializeField] private float collectSpeed;
    [SerializeField] private int craftCost;
    [SerializeField] private GameObject potionAnim;
    [SerializeField] private AudioSource boilingSource;
    public CauldronStates cauldronState;
    private InterractType interractType;
    public int requirePlant;
    private bool isPlayerInSide;
    private Animator playerAnimator;
    private float timer;
    private int leafCountRef;
    private UnityAction action;
    private bool animCallController;
    private int craftAmount;

    private IOnTaskCompleted onTaskcompleted;
    private TaskBasedAnimation taskBasedAnimation;
    private Transform collectableTarget;
    private void Start()
    {
        
        CollectablesManager.Instance.activeCauldronList.Add(transform);



        action += Interract;
        ButtonManager.Instance.wandButton.GetComponent<Button>().onClick.AddListener(action);
        playerAnimator = PlayerManager.Instance.GetComponent<Animator>();
        onTaskcompleted = GetComponent<IOnTaskCompleted>();
        taskBasedAnimation= GetComponent<TaskBasedAnimation>(); 

        
    }
    public void Interract()
    {
        if(isPlayerInSide&cauldronState is CauldronStates.Charged&CollectablesManager.Instance.LeafCount>requirePlant)
        {
            potionAnim.GetComponent<CollectablesAnimation>().targetTransform = playerAnimator.transform.GetChild(1).transform;
            collectSpeed = PlayerManager.Instance.PlayerCollectSpeed;
            craftAmount = 10;
            CollectablePool.Instance.SelectPool(PoolType.Leaf,transform);
           // leafCountRef = CollectablesManager.Instance.LeafCount;
            CollectablesManager.Instance.LeafCount -= 10;
            PlayerManager.Instance.animController.AnimationCallCount++;
            animCallController = true;
            DragonBreath(true);
            boilingSource.Play();
            DOTweenModuleAudio.DOFade(boilingSource, 0.5f, 4f);
            playerAnimator.SetBool("_Collect", true);
            ButtonManager.Instance.wandButton.SetActive(false);
            collectableTarget = PlayerManager.Instance.collectableTarget;
            interractType = InterractType.Player;
            cauldronState = CauldronStates.Cooking;

        }
    }
   

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (cauldronState == CauldronStates.Charged) ButtonManager.Instance.wandButton.SetActive(true);

            isPlayerInSide = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ButtonManager.Instance.wandButton.SetActive(false);
            isPlayerInSide = false;
        }
    }
    private void Update()
    {
        switch (cauldronState)
        {
            case CauldronStates.Cooking:
                Cooking();
               
                break;
            case CauldronStates.Charging:
                Charging();
                break;
            
        }

    }
    private void CraftPotion(int value)
    {
        
       
        CollectablesManager.Instance.PotionCount += value;
    }
    
    
    private void Cooking()
    {
        potionImage.fillAmount -= Time.deltaTime / collectSpeed;
        if (potionImage.fillAmount <= 0)
        {
            taskBasedAnimation.StartTask(onTaskcompleted,collectableTarget,interractType);
            if (animCallController)
            {
                PlayerManager.Instance.animController.AnimationCallCount--;
                animCallController = false;
            }
            
            //if (PlayerManager.Instance.animController.AnimationCallCount == 0) playerAnimator.SetBool("_Collect", false);
            DragonBreath(false);

            DOTweenModuleAudio.DOFade(boilingSource, 0f, 4f).OnComplete(() =>
            {
                boilingSource.Stop();
            });

            timer = 2f;
            cauldronState = CauldronStates.Charging;
            
          
            

        }
    }
    private void Charging()
    {
        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            
            potionImage.fillAmount += Time.deltaTime / chargeSpeed;
            if (potionImage.fillAmount >= 1f)
            {
                //potionAnim.SetActive(false);
                cauldronState = CauldronStates.Charged;
            }
        }
    }
    private void DragonBreath(bool activity)
    {
        for (int i = 0; i < dragonAnimators.Count; i++)
        {
            dragonAnimators[i].SetBool("Fire", activity);
        }
    }

    public void WorkerInterract(Transform collectablesTargetPos,float collectSpeed)
    {
        if ( cauldronState is CauldronStates.Charged & CollectablesManager.Instance.LeafCount > requirePlant)
        {
            boilingSource.Play();
            craftAmount = 5;
            collectableTarget = collectablesTargetPos;
            // potionAnim.GetComponent<CollectablesAnimation>().targetTransform = collectablesTargetPos;
            interractType = InterractType.Worker;
            this.collectSpeed = collectSpeed;
            //leafCountRef = CollectablesManager.Instance.LeafCount;
            CollectablesManager.Instance.LeafCount -= 10;
            DragonBreath(true);
            DOTweenModuleAudio.DOFade(boilingSource, 0.5f, 4f);
            cauldronState = CauldronStates.Cooking;
        }
    }

    public void OnAllTaskCompleted()
    {
        CraftPotion(craftAmount);
    }

   
    
}
