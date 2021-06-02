using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] Transform playerTransform;
    [SerializeField] float offsetZ = -15f;
    [SerializeField] float offsetX = -5f;
    [SerializeField] float constantY = 5f;
    [SerializeField] float cameraLerpTime = 0.05f;

	private void FixedUpdate()
	{
        if (playerTransform)
        {
            Vector3 targetPosition = new Vector3(playerTransform.position.x + offsetX, constantY, playerTransform.position.z + offsetZ);
            transform.position = Vector3.Lerp(transform.position, targetPosition, cameraLerpTime);
        }
	}

}
