using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnterThePortal : MonoBehaviour
{
    public string next;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            AudioSource sound = GameObject.Find("SoundEffects/Portal").GetComponent<AudioSource>();
            sound.Play();
            Invoke("goToNextScene", sound.clip.length);
            Destroy(collision.gameObject);
        }
    }

    private void goToNextScene()
    {
        SceneManager.LoadScene(next);
    }
}
