using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    public static PlayerAnimations Instance;

    private Animator _anim;

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
    }

    public void PlayerAnimRun()
    {
        _anim.SetTrigger("run");
    }

    public void PlayerAnimJump(bool isGrounded, bool isPlaying)
    {
        _anim.SetBool("isGrounded", isGrounded);
        _anim.SetBool("isPlaying", isPlaying);
    }

    public void PlayerAnimSneeze()
    {
        _anim.SetTrigger("sneeze");
    }

    public void PlayerAnimWalk(bool isWalking)
    {
        _anim.SetBool("walk", isWalking);
    }

    public void PlayerAnimIdle(bool value)
    {
        _anim.SetBool("idle", value);
    }
}