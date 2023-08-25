using UnityEngine;

public class PlayerAnimationController 
{
    
    float blend;
   
    Animator animator;


    [SerializeField]private int animationCallcount;

    public int AnimationCallCount
    {
        get { return animationCallcount ; }
        set { animationCallcount = value; OnAnimationCallOverLoad(); }
    }




    public PlayerAnimationController(Animator animator)
    {
        this.animator = animator;   
    }
    public void SetPlayerAnimations(bool isPlayerMoving)
    {
        blend = Mathf.Clamp(blend, 0, 1);
        if (isPlayerMoving)
        {
            blend += Time.deltaTime*3.5f ;
            animator.SetFloat("blend", blend);

        }
        else
        {

            blend -= Time.deltaTime*3.5f ;
            animator.SetFloat("blend", blend);
        }
    }
    public void SetConditions(string name)
    {
        animator.SetTrigger(name);

    }
    public void SetConditions(string name, bool condition)
    {
        animator.SetBool(name, condition);
    }


    private void OnAnimationCallOverLoad()
    {
        //Debug.Log(animationCallcount);
        if (animationCallcount <= 0)
        {
            animator.SetBool("_Collect", false);
            animationCallcount = 0;
        }
    }

}
