using UnityEngine;

public class Fruits : MonoBehaviour
{
    public static Fruits Instance;

    [SerializeField] private float _moveSpeed;

    private Animator _anim;
    private Rigidbody2D _rb;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        _anim = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody2D>();
        _rb.interpolation = RigidbodyInterpolation2D.Interpolate;
    }

    void FixedUpdate()
    {
        FruitMovement();
    }

    private void FruitMovement()
    {
        Vector2 target = new Vector2(-6f, -3.8f);
        transform.position = Vector2.MoveTowards(transform.position, target, _moveSpeed * Time.deltaTime);

        if (transform.position.x <= -6f)
        {
            Destroy(gameObject);
        }
    }

    public void Collected()
    {
        _anim.SetTrigger("collected");
    }

    /*-----EVENT ANIMATION-----*/
    public void DestroyFruit()
    {
        Destroy(gameObject);
    }
}
