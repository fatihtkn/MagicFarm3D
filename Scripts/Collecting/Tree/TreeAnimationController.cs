using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TreeAnimationController : MonoBehaviour
{
    private Animator _animator;
    public TaskBasedAnimation newAnim;
    
   
    private void Start()
    {
        newAnim= GetComponent<TaskBasedAnimation>();
        _animator = GetComponent<Animator>();
    }
    

    public void StartTreeAnimation()
    {
        _animator.SetTrigger("Shake");
    }
    
}
