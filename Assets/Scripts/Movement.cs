using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class Movement : MonoBehaviour
{
    private static int attempts = 1;
    
    public float maxSpeed = 10f;
    public float jumpHeight = 10f;
    public float slowingDownCoefficient = 1f;
    
    private Rigidbody2D rigidbody;
    private float horizontalInput;
    public bool isGrounded = true;
    private bool isDying;

    private AudioSource jumpSound;
    private void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        jumpSound = GameObject.Find("SoundEffects/Jump").GetComponent<AudioSource>();
    }

    private void Update()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        rigidbody.AddForce(new Vector2(horizontalInput, 0), ForceMode2D.Impulse);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rigidbody.AddForce(new Vector2(0, jumpHeight), ForceMode2D.Impulse);
            isGrounded = false;

            if (!jumpSound.isPlaying)
            {
                jumpSound.pitch = Random.Range(0.9f, 1.1f);
                jumpSound.Play();
            }
        }
        
        if (Input.GetButtonDown("Slow"))
        {
            rigidbody.AddForce(new Vector2(0, -slowingDownCoefficient * (3 * Convert.ToInt16(!isGrounded))), ForceMode2D.Impulse);
        }
        
        if (Mathf.Abs(rigidbody.velocity.x) > maxSpeed)
        {
            rigidbody.velocity = new Vector2(maxSpeed * Mathf.Sign(rigidbody.velocity.x), rigidbody.velocity.y);
        }

        if (isDying)
        {
            transform.localScale -= new Vector3(Time.deltaTime, Time.deltaTime, 0);
            Color current = GetComponent<SpriteRenderer>().color;
            current.a -= 6f * Time.deltaTime;
            GetComponent<SpriteRenderer>().color = current;
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
            AudioSource sound = GameObject.Find("SoundEffects/Hit").GetComponent<AudioSource>();
            sound.Play();
            isDying = true;
            var shaking = GameObject.Find("Main Camera").GetComponent<CameraShake>();
            shaking.enabled = true;
            shaking.shakeDuration = sound.clip.length;
            Invoke("reloadScene", sound.clip.length);
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

    private void reloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().path);
        ++attempts;
    }

    public int GetAttempts()
    {
        return attempts;
    }
}