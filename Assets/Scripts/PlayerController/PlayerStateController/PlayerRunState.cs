using UnityEngine;

public class PlayerRunState : PlayerBaseState
{
    public override void EnterState(PlayerStateManager player)
    {
        Debug.Log("Enter Run State");
    }

    public override void UpdateState(PlayerStateManager player)
    {
        if (!player.GroundedCheck && !GameManager.Instance.IsPlaying)
        {
            player.Rb.linearVelocity = new Vector2(0f, Mathf.Clamp(player.Rb.linearVelocity.y, -0.2f, float.MaxValue));
        }
        else if (player.GroundedCheck)
        {
            GameManager.Instance.StartGame();
        }

        PlayerAnimations.Instance.PlayerAnimRun();
        PlayerAnimations.Instance.PlayerAnimJump(player.GroundedCheck, GameManager.Instance.IsPlaying);

        if (Input.GetMouseButtonDown(0))
        {
            player.SwitchState(player.JumpState);
        }
    }

    public override void ExitState(PlayerStateManager player)
    {
        Debug.Log("Exit Run State");
    }
}