using UnityEngine;

public class AntiBombCollision : MonoBehaviour
{
	private bool isDestroying;
	private SpriteRenderer sprite;

	private void Start()
	{
		sprite = GetComponent<SpriteRenderer>();
	}
	private void Update()
	{
		if (isDestroying)
		{
			Color current = sprite.color;
			current.a -= 5f * Time.deltaTime;
			sprite.color = current;

			if (current.a < 0.05f)
			{
				Destroy(gameObject);
			}
		}
	}
    
	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.CompareTag("Player"))
		{
			isDestroying = true;
		}
	}
}