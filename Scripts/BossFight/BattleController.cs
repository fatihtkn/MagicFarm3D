using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class BattleController : MonoBehaviour
{
    [Header("UIGroup")]
    public GameObject monsterIcon;
    
    public Image fillableSkillImage;
    public GameObject healthBars;
    public GameObject UIGroup;

    [Header("Managers")]
    public PlayerManager playerManager;
    public BossEnemyManager enemyManager;

    [Header("Core")]
    public GameObject playerCamera;
    public GameObject BattleCam;
    public Transform playerPosition;
    public Register register;
    public GameObject forceWall;

    public float playerDamage;
    public float enemyDamage;
    public float chargedAtackDamage;

    public bool isPlayerWon;

    private UnityAction startBattleAction;
    private SkillController skillController;
    private UnityAction action;
    private void Start()
    {
        action += ChargedAtackButton;


        skillController=new SkillController(fillableSkillImage,action);
        monsterIcon.SetActive(true);
        

        startBattleAction += StartBattle;
        ButtonManager.Instance.AddListener(ButtonTypes.Battle, startBattleAction);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ButtonManager.Instance.battleButton.gameObject.SetActive(true);
            
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            transform.DOLookAt(playerManager.transform.position,1f,AxisConstraint.Y);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ButtonManager.Instance.battleButton.gameObject.SetActive(false);
        }
    }
    
    public void StartBattle()
    {
        PrepareBattleScene();
        
        skillController.StartChargeTime();
    }

    private void PrepareBattleScene()
    {
        PreSet();

        StartCoroutine(Enemy());
        

        StartCoroutine(Player());
       
    }
    
    private void PreSet()
    {
        playerManager.GetEnemy(enemyManager);
        ButtonManager.Instance.battleButton.gameObject.SetActive(false);
        UIGroup.SetActive(false);
        BattleCam.SetActive(true);
        healthBars.SetActive(true);
        monsterIcon.SetActive(false);
    }

    private IEnumerator Enemy()
    {
        yield return new WaitForSeconds(2f);
        enemyManager.animator.SetTrigger("Atack");
    }
    private IEnumerator Player()
    {
        playerManager.transform.rotation = Quaternion.Euler(0, 0, 0);
        playerManager.GetComponent<CharacterMovement>().enabled = false;
        
        playerManager.gameObject.transform.position = playerPosition.position;
        
        skillController.SetButtonActivity(true);

        yield return new WaitForSeconds(2f);
        playerManager.animController.SetConditions("Normal");
    }
    
    private void CoolDown()
    {
        DOTween.To(() => fillableSkillImage.fillAmount, x => fillableSkillImage.fillAmount = x, 1f, 6f).OnComplete(() =>
        {
            fillableSkillImage.GetComponent<Button>().enabled = true;
            fillableSkillImage.transform.parent.GetComponent<Animator>().enabled = true;
        });
    }
    
    public void ChargedAtackButton()
    {
        playerManager.animController.SetConditions("Charged");
      
        skillController.animator.enabled = false;
    }
    public void ForceWAllDissolve()
    {
        forceWall.GetComponent<Animator>().enabled = true;
       
        
       
    }

    public IEnumerator BattleResult(bool isPlayerWon)
    {
        if (isPlayerWon)
        {
           
            skillController.SetButtonActivity(false);
            UIGroup.SetActive(true);
            BattleCam.SetActive(false);
            healthBars.SetActive(false);
            playerManager.GetComponent<CharacterMovement>().enabled = true;
            playerManager.animController.SetConditions("Walk");
            register.isObjectSold = true;

            yield return new WaitForSeconds(2.3f);
            gameObject.SetActive(false);

          
            
        }
        else
        {
            
            skillController.SetButtonActivity(false);
            UIGroup.SetActive(true);
            BattleCam.SetActive(false);
            healthBars.SetActive(false);
            playerManager.GetComponent<CharacterMovement>().enabled = true;
            playerManager.animController.SetConditions("Walk");

            enemyManager.animator.SetTrigger("idle");
            

        }
    }

    private void OnDisable()
    {
        ForceWAllDissolve();
    }


}
