using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddPlatform : MonoBehaviour
{
    public GameObject prefab;
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Instantiate(prefab, Camera.main.ScreenToWorldPoint(Input.mousePosition + new Vector3(0, 0, 10)), Quaternion.identity);
        }
    }
}
