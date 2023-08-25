using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class RippleTriggerControl : MonoSingleton<RippleTriggerControl>,IOnTaskCompleted
{
    public float duration;
    public GameObject trigger;
    public ParticleSystem particle;
    public Collider col;
    private IOnTaskCompleted taskCompletedEvent;
    private void Start()
    {
        particle = transform.parent.GetComponent<ParticleSystem>();
        col= GetComponent<Collider>();
        taskCompletedEvent = GetComponent<IOnTaskCompleted>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Tree"))
        {
           TreeAnimationController treeManager = other.GetComponent<TreeAnimationController>();
            TaskBasedAnimation taskAnim = treeManager.newAnim;
            
            treeManager.StartTreeAnimation();
            StartCoroutine(WaitForEndShake(taskAnim));
            //treeManager.starAnimation.gameObject.SetActive(true);
          
        }

    }

    public void StartRipple()
    {
        col.enabled = true;
        transform.parent.transform.position = PlayerManager.Instance.transform.position;
        particle.Play();
        trigger.transform.DOScale(new Vector3(1f, 1f, 0.5f), duration).OnComplete(() =>
        {
            
            trigger.transform.localScale = Vector3.zero;
            AudioManager.Instance.PlaySound(AudioTypes.Tree);
            col.enabled = false;

        });



    }

    
    
    private IEnumerator WaitForEndShake(TaskBasedAnimation taskAnim)
    {
        yield return new WaitForSeconds(0.5f);
        //treeManager.starAnimation.gameObject.SetActive(true);
        taskAnim.StartTask(taskCompletedEvent,PlayerManager.Instance.collectableTarget,InterractType.Player);

    }

    public void OnAllTaskCompleted()
    {
        PlayerManager.Instance.starParticle.Stop();
        
    }
}
