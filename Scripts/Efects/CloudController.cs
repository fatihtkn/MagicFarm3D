
using System.Collections.Generic;
using UnityEngine;

public class CloudController 
{
	private List<Animator> animator;
	public CloudController(List<Animator> cloudAnimator)
	{
		animator = cloudAnimator;
	}
	public void SetOnTransparency()
	{
		for (int i = 0; i < animator.Count; i++)
		{
			animator[i].SetTrigger("Trigger");
		}
		
	}
	
}
