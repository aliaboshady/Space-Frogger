using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGScroller : MonoBehaviour
{
    [SerializeField] float offsetSpeed = -3f;
    Renderer myRenderer;

	private void Start()
	{
		myRenderer = GetComponent<MeshRenderer>();
	}

	private void Update()
	{
		myRenderer.material.mainTextureOffset -= new Vector2(offsetSpeed * Time.deltaTime, 0);
	}
}
