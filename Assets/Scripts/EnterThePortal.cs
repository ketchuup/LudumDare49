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
            SceneManager.LoadScene(next);
        }
    }
}
