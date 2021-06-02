using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthDamageShoot : MonoBehaviour
{
	[SerializeField] LevelGenerator levelGenerator;
	float distanceBeforeNewPlatforms = 120f;
	private void OnTriggerEnter(Collider other)
	{
		if(other.tag == Tags.MORE_PLATFORMS_TAG)
		{
			Vector3 temp = other.transform.position;
			temp.x += distanceBeforeNewPlatforms;
			other.transform.position = temp;
			levelGenerator.GenerateLevel(false);
		}
	}
}
