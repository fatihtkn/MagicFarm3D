using UnityEngine;
using DG.Tweening;
public class BuyAreaAnim : MonoBehaviour
{
   
    void Start()
    {
        transform.localScale = Vector3.zero;


    }
    private void OnEnable()
    {
        transform.DOScale(transform.localScale*1.25f , 1f).SetLoops(1, LoopType.Yoyo);
    }


}
