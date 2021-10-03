using UnityEngine;

public class AntiBombCollision : MonoBehaviour
{
	private bool isDestroying;
	private SpriteRenderer sprite;
	private AudioSource destroyingSound;

	private void Start()
	{
		sprite = GetComponent<SpriteRenderer>();
		destroyingSound = GameObject.Find("SoundEffects/AntiBombDestroyed").GetComponent<AudioSource>();
	}
	private void Update()
	{
		if (isDestroying)
		{
			if (!destroyingSound.isPlaying)
			{
				destroyingSound.Play();
			}
			
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