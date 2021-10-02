using System;
using UnityEditor;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float maxSpeed = 10f;
    public float jumpHeight = 10f;
    public float slowingDownCoefficient = 1f;
    
    private Rigidbody2D rigidbody;
    private float horizontalInput;
    private bool isGrounded = true;

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
        if ((collision.gameObject.CompareTag("Platform") || collision.gameObject.CompareTag("Ground")) && !isGrounded)
        {
            isGrounded = true;
        }
    }
    
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
}