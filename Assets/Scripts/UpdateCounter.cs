using System;
using TMPro;
using UnityEngine;

public class UpdateCounter : MonoBehaviour
{
    void Start()
    {
        int attempt = GameObject.Find("Player").GetComponent<Movement>().GetAttempts();
        GetComponent<TextMeshProUGUI>().SetText($"Attempt {attempt}");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
}