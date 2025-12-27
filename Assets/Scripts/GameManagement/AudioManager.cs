using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("Audio Sources")]
    [SerializeField] private AudioSource _musicSource;
    [SerializeField] private AudioSource _sfxSource;
    [SerializeField] private AudioSource _uiSfxSource;

    [Header("Audio Clips")]
    [SerializeField] private AudioClip _musicClip;
    [SerializeField] private AudioClip _clickedClip;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        _musicSource.clip = _musicClip;
        _musicSource.Play();
    }

    public void ClickedSound()
    {
        _uiSfxSource.PlayOneShot(_clickedClip);
    }

    public void OnOffMusic()
    {
        if (_musicSource.volume != 0)
        {
            _musicSource.volume = 0;
        }
        else
        {
            _musicSource.volume = 0.8f;
        }
    }

    public void OnOffUISfx()
    {
        if (_uiSfxSource.volume != 0)
        {
            _uiSfxSource.volume = 0;
        }
        else
        {
            _uiSfxSource.volume = 0.8f;
        }
    }
}
