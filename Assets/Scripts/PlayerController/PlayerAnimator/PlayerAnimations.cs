using System.Collections;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    public static PlayerAnimations Instance;

    public bool Immortal;
    public float _immortalCooldown;
    public float _immortalTimer;
    public float _changeAlpha;

    private Animator _anim;
    private SpriteRenderer _sprite;

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

        _sprite = GetComponent<SpriteRenderer>();
        _anim = GetComponent<Animator>();
    }

    void Start()
    {
        _immortalCooldown = 6f;
        _immortalTimer = 0f;
    }

    void Update()
    {
        SetImmortal();
        SetLayerCollision();
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

    private void SetLayerCollision()
    {
        int a = LayerMask.NameToLayer("Player");
        int b = LayerMask.NameToLayer("Obstacles");

        Physics2D.IgnoreLayerCollision(a, b, Immortal);
    }

    private void SetImmortal()
    {
        if (Immortal == false)
        {
            _sprite.color = new Color(1f, 1f, 1f, 1f);
            _immortalTimer = 0f;
            return;
        }

        _immortalTimer += Time.deltaTime;
        if (_immortalTimer <= _immortalCooldown)
        {
            _changeAlpha += Time.deltaTime;

            if (_changeAlpha > 0.2f)
            {
                _sprite.color = new Color(1f, 1f, 1f, 0.5f);
                StartCoroutine(nameof(ChangeAlpha));
            }
            else
            {
                _sprite.color = new Color(1f, 1f, 1f, 1f);
            }
        }
        else
        {
            _sprite.color = new Color(1f, 1f, 1f, 1f);
            Immortal = false;
        }
    }

    IEnumerator ChangeAlpha()
    {
        yield return new WaitForSeconds(0.2f);
        _changeAlpha = 0f;
    }
}