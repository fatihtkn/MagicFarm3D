using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;


public class FarmManager : MonoBehaviour, IInterractable,IWorkerInterract,IOnTaskCompleted
{
    [SerializeField] private Image leafImage;
     private GameObject wandButton;
   
    [SerializeField] private GameObject rainEfect;
    [SerializeField] private List<Animator> cloudAnimators;
    [SerializeField] private float chargeSpeed, collectSpeed;
    public GameObject leafAnimation;
    [SerializeField] private GameObject plants;

    private UnityAction action;

    public FarmStates currentState;
    private bool isPlayerInSide;
    private Animator playerAnimator;
    private CloudController cloudController;
    private TaskBasedAnimation taskBasedAnimation;
    private IOnTaskCompleted onTaskCompleted;
    float timer;
    public bool isWorkerInSide;
    private bool animCallController;
    private Transform collectableTarget;
    private InterractType interractType;
    private void Awake()
    {
        CollectablesManager.Instance.activeFarmList.Add(transform);
        
    }
    private void Start()
    {
        
        action += Interract;
        wandButton = ButtonManager.Instance.wandButton;
        ButtonManager.Instance.wandButton.GetComponent<Button>().onClick.AddListener(action);
        playerAnimator = PlayerManager.Instance.GetComponent<Animator>();
        cloudController = new CloudController(cloudAnimators);
        taskBasedAnimation = GetComponent<TaskBasedAnimation>();
        onTaskCompleted=GetComponent<IOnTaskCompleted>();
    }
    public void Interract()
    {
        if ( currentState is FarmStates.Charged & isPlayerInSide )
        {
            leafAnimation.GetComponent<CollectablesAnimation>().targetTransform = playerAnimator.transform.GetChild(1).transform;
            PlayerManager.Instance.animController.AnimationCallCount++;
            StartCoroutine(PlantDelay());
            collectSpeed = PlayerManager.Instance.PlayerCollectSpeed;
            animCallController = true;
            playerAnimator.SetBool("_Collect", true);
            if(wandButton!=null) wandButton.SetActive(false);
            collectableTarget = PlayerManager.Instance.collectableTarget;
            rainEfect.SetActive(true);
            interractType = InterractType.Player;
            currentState = FarmStates.Absorbing;

        }
    }
   
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (currentState == FarmStates.Charged) wandButton.SetActive(true);

            isPlayerInSide = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            wandButton.SetActive(false);
            isPlayerInSide = false;
        }
    }
    private void Update()
    {
        if (currentState == FarmStates.Absorbing)
        {
            Absorbing();

        }
        if (currentState == FarmStates.Charging)
        {
            Charging();
        }
    }

    
    private void Charging()
    {
        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            leafAnimation.SetActive(false);
            leafImage.fillAmount += Time.deltaTime / chargeSpeed;
            if (leafImage.fillAmount >= 1f)
            {
               currentState = FarmStates.Charged;
                
            }
        }
    }
    private void Absorbing()
    {
        leafImage.fillAmount -= Time.deltaTime / collectSpeed;
        if (leafImage.fillAmount <= 0)
        {

            if (animCallController)
            {
                PlayerManager.Instance.animController.AnimationCallCount--;
                animCallController = false;
            }

           
            
            timer = 2f;
            
            cloudController.SetOnTransparency();
           
            plants.SetActive(false);
            //leafAnimation.SetActive(true);
            taskBasedAnimation.StartTask(onTaskCompleted, collectableTarget,2000,interractType);
            StartCoroutine(CloudDelay());
           
            currentState = FarmStates.Charging;
        }

        
    }
    private IEnumerator CloudDelay()
    {
        yield return new WaitForSeconds(1f);
       
        rainEfect.SetActive(false);
        
        
        
    }
    private IEnumerator PlantDelay()
    {
        yield return new WaitForSeconds(1f);
        Bloom();
    }




    private void Bloom()
    {
        plants.SetActive(true);
       
    }

    public void WorkerInterract(Transform collectablesTargetPos,float collectSpeed)
    {
        if (currentState is FarmStates.Charged /*&  isWorkerInSide*/)
        {
            this.collectSpeed = collectSpeed;
            interractType = InterractType.Worker;
            //leafAnimation.GetComponent<CollectablesAnimation>().targetTransform = collectablesTargetPos;
            collectableTarget = collectablesTargetPos;
            StartCoroutine(PlantDelay());
            //wandButton.SetActive(false);

            rainEfect.SetActive(true);
            currentState = FarmStates.Absorbing;
        }
    }

    public void OnAllTaskCompleted()
    {
        CollectablesManager.Instance.LeafCount += 20;
        PlayerManager.Instance.leafParticle.Stop();
    }
}
