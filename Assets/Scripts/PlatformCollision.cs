using UnityEngine;

public class PlatformCollision : MonoBehaviour
{
   private bool isDespawning;
   private SpriteRenderer sprite;

   private void Start()
   {
      sprite = GetComponent<SpriteRenderer>();
   }

   private void Update()
   {
      if (isDespawning)
      {
         Color current = sprite.color;
         current.a -= 3f * Time.deltaTime;
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
         isDespawning = true;
      }
   }
}
