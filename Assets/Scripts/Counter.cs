using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Counter : MonoBehaviour
{
	public Image icon;
	public Number available;

	public Vector2 padding;
	public float size;
	public float spacing;
	public bool onLeft;
	
	private List<Image> images;
	
	private void Start()
	{
		images = new List<Image>();
		
		for (int i = 0; i < available.value; ++i)
		{
			Vector2 offset = Vector2.zero;
			
			if (onLeft)
			{
				offset = new Vector2(i * (size + spacing), 0);
			}
			else
			{
				offset = new Vector2(-i * (size + spacing), 0);
			}
			
			Image image = Instantiate(icon, padding + offset, Quaternion.identity);
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