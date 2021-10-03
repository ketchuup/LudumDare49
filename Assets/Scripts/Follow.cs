using UnityEngine;

public class Follow : MonoBehaviour
{
    public GameObject follow;

    void Update()
    {
        if (follow != null)
        {
            gameObject.transform.position = new Vector3(follow.transform.position.x, follow.transform.position.y, -10f);
        }
    }
}
