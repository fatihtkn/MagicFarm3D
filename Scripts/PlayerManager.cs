using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoSingleton<PlayerManager>
{
    [Header("Animation")]
    public PlayerAnimationController animController;
    private Animator playerAnimator;
    [Header("UI")]
    public Image greenBar;
    public Image redBar;
    public Transform collectableTarget;
    private BossEnemyManager _currentEnemy;
    public ParticleSystem starParticle;
    public ParticleSystem leafParticle;
    public ParticleSystem orbParticle;
    private float playerDamage;
    private float currentHealth;
    private float chargedAtackDamage;



    public SkinnedMeshRenderer characterMesh;
    public MeshRenderer wandMesh;
    public float CurrentHealth
    {
        get { return currentHealth; }
        set
        {
            currentHealth = value;
            greenBar.fillAmount = currentHealth;

            DOTween.To(() => redBar.fillAmount, x => redBar.fillAmount = x, greenBar.fillAmount, 1f);
            if (greenBar.fillAmount <= 0f)
            {
                _currentEnemy.GetComponent<BattleController>().BattleResult(false);
            }
        }
    }
    [SerializeField] private float playerCollectSpeed;

    public float PlayerCollectSpeed
    {
        get { return playerCollectSpeed; }
        set { playerCollectSpeed = value; }
    }
    public float collectSpeedRef=5f;



    private void Start()
    {
        currentHealth = 1f;
        playerAnimator = GetComponent<Animator>();
        animController = new PlayerAnimationController(playerAnimator);


    }
    private void Update()
    {

        animController.SetPlayerAnimations(CharacterMovement.Instance.IsMoving);

    }
   
    public void PlayerAtack()
    {
        playerDamage = _currentEnemy.GetComponent<BattleController>().playerDamage;
        _currentEnemy.CurrentHealth -= playerDamage;
        

    }
    public void PlayerChargedAtack()
    {
        chargedAtackDamage=_currentEnemy.GetComponent<BattleController>().chargedAtackDamage;
        _currentEnemy.CurrentHealth -= chargedAtackDamage;

    }
    public void GetEnemy(BossEnemyManager enemy)
    {
        _currentEnemy = enemy;  
    }
    public void SetCharacterMeshActivity(bool control)
    {
        characterMesh.enabled = control;
        wandMesh.enabled = control;
        

    }
}
