using NUnit.Framework;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerStateManager : MonoBehaviour
{
    SpawnObstacles _spawnObstacles;

    public float JumpForce;
    [SerializeField] private float _groundCheckDistance;
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private float _checkObstacleDistance;
    [SerializeField] private LayerMask _obstacleLayer;

    public bool GroundedCheck { get; private set; }
    public bool ObstacleCheck { get; private set; }
    public int StateIndex { get; set; }
    public float StateTimer { get; set; }
    public float StateCooldown { get; set; } = 3f;
    public bool IsHolding { get; set; }
    public Rigidbody2D Rb { get; private set; }

    PlayerBaseState currentState;
    public PlayerIdleState IdleState = new PlayerIdleState();
    public PlayerRunState RunState = new PlayerRunState();
    public PlayerJumpState JumpState = new PlayerJumpState();
    public PlayerFallState FallState = new PlayerFallState();
    public PlayerWalkState WalkState = new PlayerWalkState();
    public PlayerFlipState FlipState = new PlayerFlipState();

    void Awake()
    {
        Rb = GetComponent<Rigidbody2D>();
        _spawnObstacles = FindAnyObjectByType<SpawnObstacles>();
    }

    void Start()
    {
        currentState = IdleState;
        currentState.EnterState(this);
    }

    void Update()
    {
        GroundedCheck = IsGrounded();
        ObstacleCheck = CheckObstacle();

        if (Input.GetMouseButton(0) && GroundedCheck)
        {
            IsHolding = true;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            IsHolding = false;
        }

        currentState.UpdateState(this);
    }

    public void SwitchState(PlayerBaseState newState)
    {
        currentState.ExitState(this);
        currentState = newState;
        newState.EnterState(this);
    }

    private bool IsGrounded()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, _groundCheckDistance, _groundLayer);
        return hit.collider != null;
    }

    private bool CheckObstacle()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right, _checkObstacleDistance, _obstacleLayer);
        return hit.collider != null;
    }

    public void SpawnCrate()
    {
        _spawnObstacles.SpawnCrate();
    }

    public void Flip()
    {
        transform.Rotate(0, 180, 0);
    }

    public void FindAndDestroyCrate()
    {
        GameObject crate = GameObject.Find("Crate(Clone)");
        
        if (crate != null)
        {
            Destroy(crate);
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, Vector2.down * _groundCheckDistance);
        Gizmos.DrawRay(transform.position, Vector2.right * _checkObstacleDistance);
    }
}
