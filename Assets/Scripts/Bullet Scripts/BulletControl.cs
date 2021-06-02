using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletControl : MonoBehaviour
{
    [SerializeField] float lifeTime = 5f;
    float startY;

	private void Start()
	{
		startY = transform.position.y;
		Destroy(gameObject, lifeTime);
	}

	private void LateUpdate()
	{
		transform.position = new Vector3(transform.position.x, startY, transform.position.z);
	}

	private void OnTriggerEnter(Collider other)
	{
		if(other.tag == Tags.MONSTER_TAG || other.tag == Tags.PLAYER_TAG || other.tag == Tags.MONSTER_BULLET_TAG || other.tag == Tags.PLAYER_BULLET_TAG)
		{
			Destroy(gameObject);
		}
	}
}
