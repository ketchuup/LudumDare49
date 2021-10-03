using UnityEngine;

public class AddAntiBomb : MonoBehaviour
{
    public GameObject prefab;
    public Number available;
    void Update()
    {
        if (Input.GetMouseButtonDown(2) && available.value > 0)
        {
            --available.value;
            Instantiate(prefab, Camera.main.ScreenToWorldPoint(Input.mousePosition + new Vector3(0, 0, 10)), Quaternion.identity);
        }
    }
}
