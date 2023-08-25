using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;


public class BridgeAnimation : MonoBehaviour,IAnimateable
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
        normalScale = new Vector3(1, 1, 1);
        smallScale = normalScale / 2;
        largeScale = normalScale * 1.3f;
        
    }
    private void Update()
    {
        if (playAnim) LargeCompleted(); /*StartCoroutine(SetLargeScale());*/
        else SetNormalScale();

      

    }

    IEnumerator SetLargeScale()
    {
        transform.localScale = smallScale;
       // yield return new WaitForSeconds(0.1f);
        //transform.localScale = Vector3.Lerp(transform.localScale, largeScale,Time.deltaTime*4f);
        transform.DOScale(largeScale, 1f);
        yield return new WaitForSeconds(0f);
        playAnim = false;



    }
    private void SetNormalScale()
    {
        transform.DOScale(normalScale, 0.5f).OnComplete(() => { enabled = false; });
       
    }

    public void Animate(bool test)
    {
        playAnim = test;
        
    }
    private async Task LargeScaleAsynch(float duration) //Test
    {
        
        transform.DOScale(largeScale, 0.5f).OnComplete(() =>{ playAnim = false; });
        await Task.Yield();

        //var end = Time.time + duration;
        //if (Time.time < end)
        //{
        //    playAnim = false;
        //    await Task.Yield();

        //}

    }

    private async void LargeCompleted()//Test
    {
        await LargeScaleAsynch(2f);
    }
}
