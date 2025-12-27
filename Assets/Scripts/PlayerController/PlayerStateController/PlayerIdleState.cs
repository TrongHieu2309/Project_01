using UnityEngine;

public class PlayerIdleState : PlayerBaseState
{
    public override void EnterState(PlayerStateManager player)
    {
        Debug.Log("Enter Idle State");
        player.StateTimer = 0f;
        player.StateCooldown = 3f;
        player.StateIndex = 0;
        GameManager.Instance.IsWalking = false;
    }

    public override void UpdateState(PlayerStateManager player)
    {
        PlayerAnimations.Instance.PlayerAnimWalk(GameManager.Instance.IsWalking);
        PlayerAnimations.Instance.PlayerAnimJump(player.GroundedCheck, GameManager.Instance.IsPlaying);

        if (!GameManager.Instance.IsPlaying)
        {
            player.StateTimer += Time.deltaTime;

            if (player.StateTimer >= player.StateCooldown)
            {
                player.StateIndex++;

                if (player.StateIndex == 1)
                {
                    player.StateTimer = 0f;
                    PlayerAnimations.Instance.PlayerAnimSneeze();
                }
                else if (player.StateIndex == 2)
                {
                    player.StateTimer = 0f;
                    PlayerAnimations.Instance.PlayerAnimSneeze();
                }
                else if (player.StateIndex == 3)
                {
                    player.SwitchState(player.WalkState);
                }
            }
        }
        
        if (GameManager.Instance.IsStartGame)
        {
            player.SwitchState(player.RunState);
        }
    }

    public override void ExitState(PlayerStateManager player)
    {
        Debug.Log("Exit Idle State");
    }
}