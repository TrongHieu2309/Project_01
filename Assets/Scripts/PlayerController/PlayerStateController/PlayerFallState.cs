using UnityEngine;

public class PlayerFallState : PlayerBaseState
{
    public override void EnterState(PlayerStateManager player)
    {
        Debug.Log("Enter Fall State");
    }

    public override void UpdateState(PlayerStateManager player)
    {
        if (player.IsHolding)
        {
            player.Rb.linearVelocity = new Vector2(0f, Mathf.Clamp(player.Rb.linearVelocity.y, -0.6f, float.MaxValue));
        }
        else
        {
            player.Rb.linearVelocity = new Vector2(0f, player.Rb.linearVelocity.y);
            player.IsHolding = false;
        }

        if (!GameManager.Instance.IsPlaying)
        {
            player.Rb.linearVelocity = new Vector2(0f, Mathf.Clamp(player.Rb.linearVelocity.y, -0.4f, float.MaxValue));
        }

        if (player.GroundedCheck && GameManager.Instance.IsPlaying)
        {
            player.SwitchState(player.RunState);
        }
        else if (player.GroundedCheck && !GameManager.Instance.IsPlaying)
        {
            player.SwitchState(player.FlipState);
        }

        if (GameManager.Instance.IsStartGame)
        {
            player.SwitchState(player.RunState);
        }
    }

    public override void ExitState(PlayerStateManager player)
    {
        Debug.Log("Exit Fall State");
    }
}