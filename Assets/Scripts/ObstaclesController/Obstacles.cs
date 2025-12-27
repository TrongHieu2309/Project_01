using UnityEngine;

public class Obstacles : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _crateMoveSpeed;

    private Rigidbody2D _rb;

    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _rb.interpolation = RigidbodyInterpolation2D.Interpolate;
    }

    void FixedUpdate()
    {
        if (GameManager.Instance.IsWalking)
        {
            CrateMovement();
        }

        if (GameManager.Instance.IsPlaying)
        {
            ObstacleMovement();
        }
    }

    private void CheckDestroy()
    {
        if (transform.position.x < -6f)
        {
            Destroy(gameObject);
        }
    }

    private void ObstacleMovement()
    {
        _rb.linearVelocity = new Vector2(-1f * _moveSpeed, 0f);

        CheckDestroy();
    }

    public void CrateMovement()
    {
        _rb.linearVelocity = new Vector2(-1f * _crateMoveSpeed, 0f);

        CheckDestroy();
    }
}
