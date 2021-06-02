using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthDamageShoot : MonoBehaviour
{
	[SerializeField] LevelGenerator levelGenerator;
	[SerializeField] Transform playerBullet;
	[SerializeField] float fireDelay;
	float distanceBeforeNewPlatforms = 120f;
	float fireDelayPassed = 0;

	private void Start()
	{
		fireDelayPassed = fireDelay;
	}

	private void Update()
	{
		Fire();
		CheckFall();
	}

	void Fire()
	{
		fireDelayPassed += Time.deltaTime;
		if (Input.GetKeyDown(KeyCode.K) && fireDelayPassed >= fireDelay)
		{
			fireDelayPassed = 0f;
			Vector3 bulletPos = transform.position;
			bulletPos.y += 1.5f;
			bulletPos.x += 1f;
			Transform newBullet = Instantiate(playerBullet, bulletPos, Quaternion.identity);
			newBullet.GetComponent<Rigidbody>().AddForce(transform.forward * 1500f);
			newBullet.parent = transform;
		}
	}

	void CheckFall()
	{
		if(transform.position.y < -10f)
		{
			Destroy(gameObject);
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		if(other.tag == Tags.MORE_PLATFORMS_TAG)
		{
			Vector3 temp = other.transform.position;
			temp.x += distanceBeforeNewPlatforms;
			other.transform.position = temp;
			levelGenerator.GenerateLevel(false);
		}
		else if(other.tag == Tags.MONSTER_BULLET_TAG)
		{
			Destroy(gameObject);
		}
	}

	private void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.tag == Tags.MONSTER_TAG)
		{
			Destroy(gameObject);
		}
	}
}
