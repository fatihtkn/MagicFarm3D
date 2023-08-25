using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class BossEnemyManager : MonoBehaviour
{
    public Image greenBar;
    public Image redBar;
    [HideInInspector]public Animator animator;
    private float currentHealth;
    private float enemyDamage;
    private PlayerManager playerManager;
   
    private BattleController battleController;
	public float CurrentHealth
	{
		get { return currentHealth; }

		set 
		{

            currentHealth = value;
            greenBar.fillAmount = currentHealth;

            DOTween.To(() => redBar.fillAmount, x => redBar.fillAmount = x, greenBar.fillAmount, 1f);
            if(greenBar.fillAmount <= 0)
            {
                animator.SetTrigger("Death");
                
                StartCoroutine(battleController.BattleResult(true));
            }
        }
	}
    private void Start()
    {
        animator = GetComponent<Animator>();
        playerManager = GetComponent<BattleController>().playerManager;
        enemyDamage = GetComponent<BattleController>().enemyDamage;
        currentHealth = 1f;
        battleController = GetComponent<BattleController>();
    }


    public void EnemyAtack()
    {
        playerManager.CurrentHealth -= enemyDamage;
    }
}
