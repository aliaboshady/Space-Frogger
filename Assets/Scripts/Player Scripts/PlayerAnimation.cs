using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    public Animation anim;

	private void Start()
	{
		anim = GetComponent<Animation>();
	}

	public void DidJump()
	{
		anim.Play(Tags.JUMP_ANIMATION);
		anim.PlayQueued(Tags.JUMP_FALL_ANIMATION);
	}

	public void DidLand()
	{
		anim.Stop(Tags.JUMP_FALL_ANIMATION);
		anim.Stop(Tags.JUMP_LAND_ANIMATION);
		anim.Blend(Tags.JUMP_LAND_ANIMATION, 0);
		anim.CrossFade(Tags.RUN_ANIMATION);
	}

	public void PlayerRun()
	{
		anim.Play(Tags.RUN_ANIMATION);
	}
}
