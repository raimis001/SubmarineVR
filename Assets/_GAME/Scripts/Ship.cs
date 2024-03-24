using System.Collections;
using UnityEngine;

public class Ship : MonoBehaviour
{
    [SerializeField]
    Vector2 borders = new Vector2(-50, 50);
    [SerializeField]
    Vector2 depth = new Vector2(50, 100);
    [SerializeField]
    float speed = 5;
    [SerializeField]
    GameObject effects;

    int direction = 1;
    bool isSunk = false;
    float speedModifier = 1;

    private void Update()
    {
        if (isSunk)
            return;

        float x = direction * speed * Time.deltaTime * speedModifier;
        transform.Translate(x, 0, 0);
        Vector3 pos = transform.localPosition;
        if (pos.x < borders.x || pos.x > borders.y)
        {
            direction = (int)Mathf.Sign(Random.Range(-1f, 1f));
            pos.x = direction > 0 ? borders.x : borders.y;
            pos.z = Random.Range(depth.x, depth.y);
            speedModifier = Random.Range(1f, 1.5f);

            transform.localPosition = pos;
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (isSunk)
            return;
        Debug.Log(collision.gameObject);
        if (!collision.gameObject.CompareTag("Torpedo"))
            return;

        collision.gameObject.SetActive(false);
        effects.transform.position = collision.contacts[0].point;
        Sunk();
    }

    void Sunk()
    {
        isSunk = true;
        StartCoroutine(ISunk());
    }

    IEnumerator ISunk()
    {
        effects.SetActive(true);
        while (true)
        {
            yield return null;
            transform.Translate(0, -Time.deltaTime, 0);
            if (transform.localPosition.y < -3f)
                break;
        }

        yield return new WaitForSeconds(1);

        Vector3 pos = Vector3.zero;

        direction = (int)Mathf.Sign(Random.Range(-1f, 1f));
        pos.x = direction > 0 ? borders.x : borders.y;
        pos.z = Random.Range(depth.x, depth.y);
        speedModifier = Random.Range(1f, 1.5f);

        transform.localPosition = pos;

        effects.SetActive(false);
        isSunk = false;
    }
}
