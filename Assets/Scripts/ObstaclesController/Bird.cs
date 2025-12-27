using UnityEngine;

public class Bird : MonoBehaviour
{
    [SerializeField] private float _flySpeed;

    void Update()
    {
        Vector3 target = new Vector3(-6f, transform.position.y, 0f);
        transform.position = Vector3.MoveTowards(transform.position, target, _flySpeed * Time.deltaTime);

        if (transform.position.x <= -6f)
        {
            Destroy(gameObject);
        }
    }
}