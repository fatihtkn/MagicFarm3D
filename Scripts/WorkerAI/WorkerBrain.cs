using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WorkerBrain : MonoBehaviour
{   
   
    public FarmManager[] farms;
    
    public CauldronManager[] cauldrons;

    public float collectSpeed;

    private WorkerStates currentState;
    public NavMeshAgent agent;
    private Vector3 targetPos;
    public WorkerStates test;
    
    private void Start()
    {
        //agent=GetComponent<NavMeshAgent>();
        
        currentState = WorkerStates.MovingToFarm;
        
        targetPos = farms[0].transform.position;
        
    }
    private void Update()
    {
        test = currentState;

        switch (currentState)
        {
            case WorkerStates.MovingToFarm:
                SetWorkerDestination(targetPos);
                break;
            case WorkerStates.MovingToCauldron:
                SetWorkerDestination(targetPos);
                break;
            case WorkerStates.CollectingLeafs:
                break;
            case WorkerStates.CraftingPotion:
                break;
            
        }
       
    }
    private void OnTriggerStay(Collider other)//was Stay
    {
        if (other.gameObject.CompareTag("Farm") & currentState is WorkerStates.MovingToFarm)
        {
            StartCoroutine(CollectLeaf(other));


        }
        if (other.gameObject.CompareTag("Cauldron") & currentState is WorkerStates.MovingToCauldron)
        {

            StartCoroutine(CraftPotion(other));
        }
    }

    private IEnumerator CollectLeaf(Collider other)
    {
        if (other.gameObject.GetComponent<FarmManager>().currentState == FarmStates.Charged)
        {
           
          
           
            agent.isStopped = true;

            other.gameObject.GetComponent<IWorkerInterract>().WorkerInterract(transform,collectSpeed);
            yield return new WaitForSeconds(collectSpeed);
            currentState = WorkerStates.MovingToCauldron;
            FindAvailableCauldron();
            agent.isStopped = false;
        }
        else
        {
            Test();
           
        }
        
       
       
    }
    private IEnumerator CraftPotion(Collider other)
    {
        if (other.gameObject.GetComponent<CauldronManager>().cauldronState == CauldronStates.Charged)
        {
            
          
            agent.isStopped = true;
            other.gameObject.GetComponent<IWorkerInterract>().WorkerInterract(transform,collectSpeed);
            yield return new WaitForSeconds(collectSpeed);
            currentState = WorkerStates.MovingToFarm;
            Test();
            agent.isStopped = false;
           
        }
        else
        {
            FindAvailableCauldron();
        }
        
    }

   
    private void SetWorkerDestination(Vector3 pos)
    {
        agent.SetDestination(pos);
    }
    
    //private void FindClosestDestination( List<Transform> targetList)
    //{
    //    Vector3 targetPos = destinationCalculator.DestinationOutput(transform, targetList);
    //    this.targetPos= targetPos;
        
    //}
    private void Test()
    {
        //targetPos = farms[0].transform.position;

        int counter = 0;

        for (int i = 0; i < farms.Length; i++)
        {
            if (farms[i].currentState == FarmStates.Charged)
            {
                targetPos =  farms[i].transform.position;
               
                return;
               
            }
            else
            {
                counter++;
            }
           

        }
        if(counter == farms.Length)
        {
            
            targetPos = farms[0].transform.position;
            
        }
        

    }

    

    private void FindAvailableCauldron()
    {
        //targetPos = cauldrons[0].transform.position;
        int counter = 0;
        for (int i = 0; i < cauldrons.Length; i++)
        {
            if (cauldrons[i].cauldronState == CauldronStates.Charged)
            {
                targetPos = cauldrons[i].transform.position;
                
                return;
            }
            else
            {
                
                counter++;
            }
            

        }
        if(counter == cauldrons.Length)
        {
            
            targetPos = cauldrons[0].transform.position;
        }
    }
}
