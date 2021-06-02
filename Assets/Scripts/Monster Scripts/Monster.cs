using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    [SerializeField] GameObject monsterDiedEffect;
    [SerializeField] Transform bullet;
    [SerializeField] float distanceFromPlayerToStartMove = 20f;
    [SerializeField] float movementSpeedMin = 1f;
    [SerializeField] float movementSpeedMax = 2f;
    [SerializeField] bool canShoot;

	Transform playerTransform;
	bool moveRight;
    bool isPlayerInRegion;
    float movementSpeed;
    
	private void Start()
	{
		if(Random.Range(0.0f, 1.0f) > 0.5f)
		{
            moveRight = true;
		}
		else
		{
            moveRight = false;
		}

        movementSpeed = Random.Range(movementSpeedMin, movementSpeedMax);
		playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
	}

	private void Update()
	{
		if (playerTransform)
		{
			float distanceFromPlayer = (playerTransform.position - transform.position).magnitude;
			if(distanceFromPlayer < distanceFromPlayerToStartMove)
			{
				if (!isPlayerInRegion)
				{
					isPlayerInRegion = true;
					if (canShoot)
					{
						InvokeRepeating("StartShooting", 0.5f, 1.5f);
					}
				}

				if (moveRight)
				{
					transform.position = new Vector3(transform.position.x + movementSpeed * Time.deltaTime, transform.position.y, transform.position.z);
				}
				else
				{
					transform.position = new Vector3(transform.position.x - movementSpeed * Time.deltaTime, transform.position.y, transform.position.z);
				}
			}
			else
			{
				CancelInvoke("StartShooting");
			}
		
		}
	}

	void StartShooting()
	{
		if (playerTransform)
		{
			Vector3 bulletPos = transform.position;
			bulletPos.y += 1.5f;
			bulletPos.x -= 1f;
			Transform newBullet = Instantiate(bullet, bulletPos, Quaternion.identity);
			newBullet.GetComponent<Rigidbody>().AddForce(transform.forward * 1500f);
			newBullet.parent = transform;
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		if(other.tag == Tags.PLAYER_BULLET_TAG)
		{
			MonsterDie();
		}
	}

	private void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.tag == Tags.PLAYER_TAG)
		{
			//MonsterDie();
		}
	}

	void MonsterDie()
	{
		Vector3 effectPos = transform.position;
		effectPos.y += 2f;
		Instantiate(monsterDiedEffect, effectPos, Quaternion.identity);
		Destroy(gameObject);
	}
}
