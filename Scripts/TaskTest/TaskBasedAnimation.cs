using DG.Tweening;
using System.Collections;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

public class TaskBasedAnimation : MonoBehaviour
{
    public TaskAnim[] targetCollectable;
    [Header("Transition Values")]
    public float duration = 1f;

    [Range(100, 2000)] public int trackingStartDelay = 500;
    [SerializeField] private int transitionStartDelay = 1000;

    [Header("Force")]
    [SerializeField] bool isRandom;
    [SerializeField] float sideForce;
    [SerializeField] float upForce;
    Task[] currentTasks;
    [SerializeField] CollecTableType collectableType;
   
    public async void StartTask(IOnTaskCompleted taskCompleted,Transform targetPos, InterractType interractType)
    {
        var tasks = new Task[targetCollectable.Length];
        currentTasks = tasks;
        DecideCollectableEnableType();
        for (var i = 0; i < targetCollectable.Length; i++)
        {
              
            tasks[i] = targetCollectable[i].RotateForSeconds(duration,targetPos,  interractType);
            await Task.Delay(trackingStartDelay);
        }

        await Task.WhenAll(tasks);
        taskCompleted.OnAllTaskCompleted();
        
    }
    public async void StartTask(Transform targetPos)
    {
        var tasks = new Task[targetCollectable.Length];
        currentTasks = tasks;
        DecideCollectableEnableType();
        for (var i = 0; i < targetCollectable.Length; i++)
        {

            tasks[i] = targetCollectable[i].RotateForSeconds(duration,targetPos,InterractType.Player);
            await Task.Delay(trackingStartDelay);
        }

        await Task.WhenAll(tasks);
    }
    public async void StartTask(IOnTaskCompleted taskCompleted, Transform targetPos,int delay, InterractType interractType)
    {
        var tasks = new Task[targetCollectable.Length];
        currentTasks = tasks;
        DecideCollectableEnableType();
        await Task.Delay(transitionStartDelay);
        for (var i = 0; i < targetCollectable.Length; i++)
        {

            tasks[i] = targetCollectable[i].RotateForSeconds(duration, targetPos,interractType);
            await Task.Delay(trackingStartDelay);
        }

        await Task.WhenAll(tasks);
        taskCompleted.OnAllTaskCompleted();
    }



    void EnableCollectablesInstant()
    {
        for (int i = 0; i < targetCollectable.Length; i++)
        {
           
            targetCollectable[i].gameObject.SetActive(true);
            targetCollectable[i].transform.parent = null;
            targetCollectable[i].SetGravityActivity(true);
            targetCollectable[i].AddVelocity(sideForce, upForce);
        }
        
    }
   async void EnableCollectableWithDelay()
   {
        for (int i = 0; i < targetCollectable.Length; i++)
        {

            targetCollectable[i].gameObject.SetActive(true);
            targetCollectable[i].transform.parent = null;
            targetCollectable[i].SetGravityActivity(true);
            targetCollectable[i].AddVelocity(sideForce, upForce);
            await Task.Delay(100);
        }
        

   }
    async void EnableCollectablesTransitionDelay()
    {
        for (int i = 0; i < targetCollectable.Length; i++)
        {

            targetCollectable[i].gameObject.SetActive(true);
            targetCollectable[i].transform.parent = null;
            targetCollectable[i].SetGravityActivity(true);
            targetCollectable[i].AddVelocity(sideForce, upForce);
            await Task.Delay(100);
        }
        
    }
    void DecideCollectableEnableType()
    {
        switch (collectableType)
        {
            
            case CollecTableType.Default:
                EnableCollectableWithDelay();
                break;
               
            case CollecTableType.Star:
                EnableCollectablesInstant();
                break;
            case CollecTableType.Leaf:
                EnableCollectablesTransitionDelay();
                break;


        }
    }
    public void OnCollectableCharged()
    {
        for (int i = 0; i < targetCollectable.Length; i++)
        {
            targetCollectable[i].gameObject.SetActive(true);
            targetCollectable[i].transform.DOScale(0, 1f).From();
        }
    }
    public async void OnAllTaskCompleted(IOnTaskCompleted completedTask)//test
    {
        await Task.WhenAll(currentTasks);
        completedTask.OnAllTaskCompleted();
    }
   

   
}
