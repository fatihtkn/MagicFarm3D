using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarFarmScaleAnimation : MonoBehaviour
{

   
    private Vector3 normalScale;
   
    private Vector3 largeScale;
  
    public GameObject farm;

    private void Awake()
    {
        
        

        normalScale= transform.localScale;
        largeScale = normalScale * 1.4f;
        // transform.localScale = smallScale;
        transform.localScale = Vector3.zero;
    }
    private void Start()
    {
        transform.DOScale(largeScale, 1f).OnComplete(() =>
        {
            transform.DOScale(normalScale, 0.5f).OnComplete(() =>
            {
                farm.SetActive(true);
                gameObject.SetActive(false);
            });
        });
        // normalScale = transform.localScale;
        // smallScale = normalScale * 0.5f;
        // largeScale = normalScale * 1.4f;
        //

    }
    private void Update()
    {

       // if (playAnim) StartCoroutine(SetLargeScale());
       // else StartCoroutine(SetNormalScale());
       //


    }

  

   
}
