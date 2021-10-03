using UnityEngine;

public class BombCollision : MonoBehaviour
{
    private bool isExploding;
    private GameObject player;
    private SpriteRenderer sprite;
    private AudioSource explosion;

    private void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        explosion = GameObject.Find("SoundEffects/Explosion").GetComponent<AudioSource>();
    }
    private void Update()
    {
        if (isExploding)
        {
            if (!explosion.isPlaying)
            {
                explosion.PlayDelayed(0.01f);
            }
            
            transform.localScale = transform.localScale + new Vector3(Time.deltaTime, Time.deltaTime, 0.0f);
            
            Color current = sprite.color;
            current.a -= 6f * Time.deltaTime;
            sprite.color = current;
            
            if (current.a < 0.1f)
            {
                Vector2 direction = transform.position - player.transform.position;
                if (direction.magnitude < 3f)
                {
                    player.GetComponent<Rigidbody2D>().AddForce(-direction.normalized * 10f, ForceMode2D.Impulse);
                    player.GetComponent<Movement>().isGrounded = false;
                }
                Destroy(gameObject);
            }
        }
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isExploding = true;
            player = collision.gameObject;
        }
    }
}
