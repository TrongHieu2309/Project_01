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
    [SerializeField] private AudioClip _jumpClip;
    [SerializeField] private AudioClip _clickedClip;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        
        Instance = this;
        DontDestroyOnLoad(gameObject);
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

    public void JumpSound()
    {
        _sfxSource.PlayOneShot(_jumpClip);
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

    public void OnOffSfx()
    {
        if (_sfxSource.volume != 0)
        {
            _sfxSource.volume = 0;
        }
        else
        {
            _sfxSource.volume = 1f;
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
            _uiSfxSource.volume = 0.4f;
        }
    }

    public bool Music()
    {
        if (_musicSource.volume == 0)
        {
            return false;
        }
        return true;
    }
    
    public bool Sfx()
    {
        if (_sfxSource.volume == 0)
        {
            return false;
        }
        return true;
    }

    public bool UISfx()
    {
        if (_uiSfxSource.volume == 0)
        {
            return false;
        }
        return true;
    }
}
