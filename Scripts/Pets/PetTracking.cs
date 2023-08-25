using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PetTracking : MonoBehaviour
{
    public NavMeshAgent agent;
    [SerializeField] Transform playerPos;
    private Animator animator;
    public Animals animal;
    public bool startTrack;
    private void Awake()
    {
        animator = GetComponent<Animator>();
       
    }
    private void Update()
    {

        if (startTrack)
        {
            agent.SetDestination(playerPos.position);

            DistanceControl();
        }
        
       

    }
    private void DistanceControl()
    {
        if (animal != Animals.Owl)
        {
            if (agent.remainingDistance < agent.stoppingDistance)
            {
                agent.isStopped = true;
                animator.SetBool("Run", false);
            }
            else
            {
                agent.isStopped = false;
                animator.SetBool("Run", true);
            }
        }
    }


    public void StartTrack(bool control)
    {
        agent.enabled = control;
        startTrack = control;
    }
    
}
