using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class SkillController 
{
	private Image skillImage;
	public Animator animator;
	private Button button;
	public SkillController(Image fillableImage,UnityAction buttonAction)
	{
		
		skillImage = fillableImage;
		animator = fillableImage.transform.parent.GetComponent<Animator>();
		button = fillableImage.GetComponent<Button>();
		button.onClick.AddListener(buttonAction);
        skillImage.fillAmount = 0f;
    }




	public void StartChargeTime()
	{
        DOTween.To(() => skillImage.fillAmount, x =>skillImage.fillAmount = x, 1f, 6f).OnComplete(() =>
        {
			 button.enabled = true;
			animator.enabled = true;
        });
    }
	public void SetButtonActivity(bool control)
	{
        skillImage.transform.parent.gameObject.SetActive(control);
    }
	
}
