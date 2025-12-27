using UnityEngine;

public class Parallax : MonoBehaviour
{
    [SerializeField] private float _parallaxEffectRun;
    [SerializeField] private float _parallaxEffectWalk;

    private float offset;

    private Material _mat;
    private PlayerStateManager player;

    void Awake()
    {
        player = FindAnyObjectByType<PlayerStateManager>();
        _mat = GetComponent<Renderer>().material;
    }

    void Update()
    {
        if (GameManager.Instance.IsWalking)
        {
            ParallaxWalk();
            return;
        }

        if (GameManager.Instance.IsPlaying)
        {
            ParallaxRun();
            return;
        }
    }

    private void ParallaxRun()
    {
        offset += Time.deltaTime * _parallaxEffectRun;
        _mat.SetTextureOffset("_MainTex", Vector2.right * offset);
    }
    
    private void ParallaxWalk()
    {
        offset += Time.deltaTime * _parallaxEffectWalk;
        _mat.SetTextureOffset("_MainTex", Vector2.right * offset);
    }
}
