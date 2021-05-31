using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFootSmoke : MonoBehaviour
{
	[SerializeField] GameObject smokeEffect;
	[SerializeField] GameObject smokePosition;
	private void OnTriggerEnter(Collider other)
	{
		if(other.tag == "Platform")
		{
			GameObject smoke = Instantiate(smokeEffect, smokePosition.transform.position, Quaternion.identity);
			Destroy(smoke, 1.5f);
		}
	}
}
