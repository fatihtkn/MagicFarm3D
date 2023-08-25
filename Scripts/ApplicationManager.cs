using UnityEngine;
using DG.Tweening;
public class ApplicationManager : MonoBehaviour
{
    private void Awake()
    {
        DOTween.SetTweensCapacity(3150, 50);
    }
    private void Start()
    {
       
       Screen.SetResolution(Screen.currentResolution.width, Screen.currentResolution.height, true);
        Application.targetFrameRate = 60;
    }
}
