using UnityEngine;

public class PlayerFlipState : PlayerBaseState
{
    private float _flipCooldown;
    private float _flipTimer;

    public override void EnterState(PlayerStateManager player)
    {
        Debug.Log("Enter Flip State");
        player.Flip();
        _flipCooldown = 1.5f;
        _flipTimer = 0f;
        GameManager.Instance.IsWalking = false;
    }

    public override void UpdateState(PlayerStateManager player)
    {
        PlayerAnimations.Instance.PlayerAnimIdle(true);
        PlayerAnimations.Instance.PlayerAnimWalk(GameManager.Instance.IsWalking);

        if (_flipTimer < _flipCooldown)
        {
            _flipTimer += Time.deltaTime;
        }
        else if (_flipTimer >= _flipCooldown)
        {
            player.SwitchState(player.IdleState);
        }
    }

    public override void ExitState(PlayerStateManager player)
    {
        Debug.Log("Exit Flip State");
        PlayerAnimations.Instance.PlayerAnimIdle(false);
        player.Flip();
    }
}