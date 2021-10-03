using UnityEngine;

public class BombCollision : MonoBehaviour
{
    private bool isExploding;
    private GameObject player;
    private SpriteRenderer sprite;

    private void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
    }
    private void Update()
    {
        if (isExploding)
        {
            Color current = sprite.color;
            current.a -= 2f * Time.deltaTime;
            sprite.color = current;

            if (current.a < 0.05f)
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
