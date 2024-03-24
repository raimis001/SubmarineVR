using UnityEngine;

public class Torpedo : MonoBehaviour
{
    public float speed = 20;

    void Update()
    {
        transform.Translate(transform.forward * speed * Time.deltaTime, Space.World);
        if (transform.position.z > 150)
            gameObject.SetActive(false);
    }
}
