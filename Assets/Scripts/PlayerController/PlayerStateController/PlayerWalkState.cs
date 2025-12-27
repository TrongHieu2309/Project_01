using UnityEditor;
using UnityEngine;

public class PlayerWalkState : PlayerBaseState
{
    public override void EnterState(PlayerStateManager player)
    {
        Debug.Log("Enter Walk State");
        GameManager.Instance.IsWalking = true;
        player.StateTimer = 0f;
        player.StateCooldown = 6f;
        player.SpawnCrate();
    }

    public override void UpdateState(PlayerStateManager player)
    {
        PlayerAnimations.Instance.PlayerAnimWalk(GameManager.Instance.IsWalking);

        if (player.ObstacleCheck)
        {
            player.SwitchState(player.JumpState);
            return;
        }

        if (player.StateTimer < player.StateCooldown)
        {
            player.StateTimer += Time.deltaTime;
        }
        else if (player.StateTimer >= player.StateCooldown)
        {
            player.SwitchState(player.IdleState);
        }

        if (GameManager.Instance.IsStartGame)
        {
            player.FindAndDestroyCrate();
            player.SwitchState(player.RunState);
        }
    }

    public override void ExitState(PlayerStateManager player)
    {
        Debug.Log("Exit Walk State");
    }
}