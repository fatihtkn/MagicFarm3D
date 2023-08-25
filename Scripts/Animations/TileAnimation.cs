using DG.Tweening;
using System.Collections;
using UnityEngine;

public class TileAnimation : MonoBehaviour,IAnimateable
{
    private bool playAnim;
    private Vector3 normalScale;
    private Vector3 smallScale;
    private Vector3 largeScale;
   
    private void Awake()
    {
        
      
        transform.localScale = smallScale;
    }
    private void Start()
    {
       
        normalScale = new Vector3(100,100,100);
        smallScale = normalScale* 0.5f;
        largeScale = normalScale * 1.4f;
        
       
    }
    private void Update()
    {
       
        if (playAnim) StartCoroutine(SetLargeScale());
        else StartCoroutine(SetNormalScale());



    }
    
    IEnumerator SetLargeScale()
    {
        //transform.localScale = smallScale;
        transform.DOScale(largeScale, 1f);
        yield return new WaitForSeconds(0.6f);
       
        playAnim = false;

    }
    IEnumerator SetNormalScale()
    {
        
        transform.DOScale(normalScale, 0.5f);
        yield return new WaitForSeconds(2.1f);
        
        this.enabled = false;

    }

    public void Animate(bool test)
    {
        playAnim = test;
    }
}
