using System;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float maxSpeed = 10f;
    public float jumpHeight = 10f;
    public float slowingDownCoefficient = 1f;
    
    private Rigidbody2D rigidbody;
    private float horizontalInput;
    public bool isGrounded = true;

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        rigidbody.AddForce(new Vector2(horizontalInput, 0), ForceMode2D.Impulse);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rigidbody.AddForce(new Vector2(0, jumpHeight), ForceMode2D.Impulse);
            isGrounded = false;
        }
        
        if (Input.GetButtonDown("Slow"))
        {
            rigidbody.AddForce(new Vector2(0, -slowingDownCoefficient * (3 * Convert.ToInt16(!isGrounded))), ForceMode2D.Impulse);
        }
        
        if (Mathf.Abs(rigidbody.velocity.x) > maxSpeed)
        {
            rigidbody.velocity = new Vector2(maxSpeed * Mathf.Sign(rigidbody.velocity.x), rigidbody.velocity.y);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if ((collision.gameObject.CompareTag("AntiBomb") || collision.gameObject.CompareTag("Bomb") || collision.gameObject.CompareTag("Platform") || collision.gameObject.CompareTag("Ground")) && !isGrounded)
        {
            isGrounded = true;
        }
        else if (collision.gameObject.CompareTag("Hurting"))
        {
            Destroy(gameObject);
        }
    }
    
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }

    private void OnTriggerStay2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Field"))
        {
            Vector2 direction = transform.position - collider.gameObject.transform.position;
            rigidbody.AddForce(-direction.normalized * 0.5f, ForceMode2D.Impulse);
        }
    }
}