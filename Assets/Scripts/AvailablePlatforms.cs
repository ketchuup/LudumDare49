using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AvailablePlatforms : MonoBehaviour
{
	public Image platform;
	public Number available;

	public Vector2 padding;
	public float size;
	public float spacing;
	
	private List<Image> images;
	
	private void Start()
	{
		images = new List<Image>();
		
		for (int i = 0; i < available.value; ++i)
		{
			Image image = Instantiate(platform, padding + new Vector2(i * (size + spacing), 0), Quaternion.identity);
			image.transform.SetParent(transform, false);
			images.Add(image);
		}
	}
	private void Update()
	{
		for (int i = available.value; i < images.Count; ++i)
		{
			Destroy(images[i].gameObject);
			images.RemoveAt(i);
		}
	}
}