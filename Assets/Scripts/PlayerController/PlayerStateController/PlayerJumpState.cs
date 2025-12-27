using UnityEngine;

public class PlayerJumpState : PlayerBaseState
{
    public override void EnterState(PlayerStateManager player)
    {
        player.Rb.AddForce(Vector2.up * player.JumpForce, ForceMode2D.Impulse);
    }

    public override void UpdateState(PlayerStateManager player)
    {
        PlayerAnimations.Instance.PlayerAnimJump(player.GroundedCheck, GameManager.Instance.IsPlaying);

        if (player.Rb.linearVelocity.y < 0f)
        {
            player.SwitchState(player.FallState);
        }

        if (GameManager.Instance.IsStartGame)
        {
            player.SwitchState(player.RunState);
        }
    }

    public override void ExitState(PlayerStateManager player)
    {
        Debug.Log("Exit Jump State");
    }
}