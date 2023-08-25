using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class CrystalFarmManager : MonoBehaviour, IInterractable,IOnTaskCompleted
{
    
    public GameObject trail;
    private Animator playerAnimator;

    private CrystalStates crystalState;
    private CrystalAnimation crystalAnimation;

    [SerializeField] private Animator crystalAnimator;
    [SerializeField] private Image crystalImage;
    [SerializeField] private GameObject createOrb;
    [SerializeField] private GameObject electricity;

    [SerializeField] private TaskBasedAnimation taskBasedAnimation;

    private IOnTaskCompleted onTaskCompleted;
    private bool isPlayerInSide;
    private float timer;
    private UnityAction action;
    public float collectSpeed = 4f;
    public float chargeSpeed = 4f;
    private void Start()
    {

        action += Interract;
        crystalAnimation =new CrystalAnimation(crystalAnimator);
        playerAnimator = PlayerManager.Instance.GetComponent<Animator>();
        crystalState = CrystalStates.Charged;
        onTaskCompleted=GetComponent<IOnTaskCompleted>();
    }
    public void Interract()
    {
        if (isPlayerInSide & crystalState is CrystalStates.Charged)
        {
            electricity.SetActive(true);

            PlayerManager.Instance.animController.AnimationCallCount++;
            collectSpeed = PlayerManager.Instance.PlayerCollectSpeed;
            trail.SetActive(true);
            playerAnimator.SetBool("_Collect", true);
            ButtonManager.Instance.wandButton.SetActive(false);
            crystalState = CrystalStates.Absorbing;
        }


    }
    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Player"))
        {
            ButtonManager.Instance.wandButton.GetComponent<Button>().onClick.AddListener(action);
        }
        
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (crystalState == CrystalStates.Charged) ButtonManager.Instance.wandButton.SetActive(true);

            isPlayerInSide = true;
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ButtonManager.Instance.wandButton.SetActive(false);
            isPlayerInSide = false;
            
            ButtonManager.Instance.wandButton.GetComponent<Button>().onClick.RemoveListener(action);
        }
    }
    private void Update()
    {

        if (crystalState == CrystalStates.Absorbing)
        {
            Absorbing();

        }
        if (crystalState == CrystalStates.Charging)
        {
            Charging();
        }
        
        

    }
    private void Charging()
    {
        timer -= Time.deltaTime;
        if(timer <= 0f)
        {
            
            crystalImage.fillAmount += Time.deltaTime / chargeSpeed;
            if (crystalImage.fillAmount >= 1f)
            {
                Charged();
            }
        }
    }
    private void Absorbing()
    {
        
        crystalImage.fillAmount -= Time.deltaTime / collectSpeed;

        crystalAnimation.SetGlowEfect(false);
        if (crystalImage.fillAmount <= 0f)
        {
            PlayerManager.Instance.animController.AnimationCallCount--;
            electricity.SetActive(false);
            trail.SetActive(false);
            // if (PlayerManager.Instance.animController.AnimationCallCount== 0) playerAnimator.SetBool("_Collect", false);

            //createOrb.SetActive(true);
            taskBasedAnimation.StartTask(onTaskCompleted, PlayerManager.Instance.collectableTarget,InterractType.Player);
            
            crystalState = CrystalStates.Charging;
            timer = 2f;
            

        }
    }
    private void Charged()
    {
        //createOrb.SetActive(false);
        crystalAnimation.SetGlowEfect(true);
        
        crystalState = CrystalStates.Charged;

    }

    public void OnAllTaskCompleted()
    {
        CollectablesManager.Instance.OrbCount += 10;
       
    }
   

    public enum CrystalStates
    {
        Charged,
        Absorbing,
        Charging

    }
}
