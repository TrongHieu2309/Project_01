using System.ComponentModel.Design.Serialization;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class GameUI : MonoBehaviour
{
    private int _score;
    private float _scoreTimer;

    private Label _scoreText, _currentScoreText, _bestScoreTextInPause, _highScoreTextInGameOver;
    private Button _optionsButton, _playButton, _pauseGameButton, _settingsButton, _closeSettingsButton,
    _onOffMusicBtn, _onOffSfxBtn, _onOffUISfxBtn, _resumeButton, _restartButton, _homeButton;
    private VisualElement _gamePlayUI, _optionsPanel, _homePanel, _settingsPanel, _pauseGamePanel, _gameOverPanel;

    void Awake()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;

        _scoreText = root.Q<Label>("ScoreTxt");
        _currentScoreText = root.Q<Label>("CurrentScoreTxt");
        _bestScoreTextInPause = root.Q<Label>("BestScoreTxt");
        _highScoreTextInGameOver = root.Q<Label>("HighScoreTxt");

        _gamePlayUI = root.Q<VisualElement>("GamePlayUI");
        _optionsPanel = root.Q<VisualElement>("OptionsPanel");
        _homePanel = root.Q<VisualElement>("HomePanel");
        _settingsPanel = root.Q<VisualElement>("SettingsPanel");
        _pauseGamePanel = root.Q<VisualElement>("PauseGamePanel");
        _gameOverPanel = root.Q<VisualElement>("GameOverPanel");

        _optionsButton = root.Q<Button>("OptionsBtn");
        _optionsButton.RegisterCallback<ClickEvent>(OnClickOptions);

        _playButton = root.Q<Button>("PlayBtn");
        _playButton.RegisterCallback<ClickEvent>(OnClickPlay);

        _pauseGameButton = root.Q<Button>("PauseGameBtn");
        _pauseGameButton.RegisterCallback<ClickEvent>(OnClickPauseGame);

        _settingsButton = root.Q<Button>("SettingsBtn");
        _settingsButton.RegisterCallback<ClickEvent>(OnClickSettings);

        _onOffMusicBtn = root.Q<Button>("OnOffMusicBtn");
        _onOffMusicBtn.RegisterCallback<ClickEvent>(OnClickMusic);

        _onOffSfxBtn = root.Q<Button>("OnOffSfxBtn");
        _onOffSfxBtn.RegisterCallback<ClickEvent>(OnClickSfx);

        _onOffUISfxBtn = root.Q<Button>("OnOffUISfxBtn");
        _onOffUISfxBtn.RegisterCallback<ClickEvent>(OnClickUISfx);

        _closeSettingsButton = root.Q<Button>("CloseSettingsBtn");
        _closeSettingsButton.RegisterCallback<ClickEvent>(OnClickSettings);

        _resumeButton = root.Q<Button>("ResumeBtn");
        _resumeButton.RegisterCallback<ClickEvent>(OnClickPauseGame);

        _restartButton = root.Q<Button>("RestartBtn");
        _restartButton.RegisterCallback<ClickEvent>(OnClickRestart);

        _homeButton = root.Q<Button>("HomeBtn");
        _homeButton.RegisterCallback<ClickEvent>(OnClickRestart);
    }

    void Start()
    {
        _optionsPanel.RemoveFromClassList("OptionsPanelActive");

        _bestScoreTextInPause.text = PlayerPrefs.GetInt("Best Score", 0).ToString();
        _highScoreTextInGameOver.text =  PlayerPrefs.GetInt("Best Score", 0).ToString();

        _homePanel.style.display = DisplayStyle.Flex;
        _gamePlayUI.style.display = DisplayStyle.None;
        _settingsPanel.AddToClassList("PanelHidden");
        _pauseGamePanel.AddToClassList("PanelHidden");
        _gameOverPanel.AddToClassList("PanelHidden");

        if (AudioManager.Instance.Music())
        {
            _onOffMusicBtn.text = "OFF";
            _onOffMusicBtn.style.color = Color.white;
            _onOffMusicBtn.style.unityBackgroundImageTintColor = new Color(0.518f, 0.518f, 0.518f, 1);
        }
        else
        {
            _onOffMusicBtn.text = "ON";
            _onOffMusicBtn.style.color = Color.black;
            _onOffMusicBtn.style.unityBackgroundImageTintColor = new Color(1, 1, 1, 1);
        }
        
        if (AudioManager.Instance.Sfx())
        {
            _onOffSfxBtn.text = "OFF";
            _onOffSfxBtn.style.color = Color.white;
            _onOffSfxBtn.style.unityBackgroundImageTintColor = new Color(0.518f, 0.518f, 0.518f, 1);
        }
        else
        {
            _onOffSfxBtn.text = "ON";
            _onOffSfxBtn.style.color = Color.black;
            _onOffSfxBtn.style.unityBackgroundImageTintColor = new Color(1, 1, 1, 1);
        }
        
        if (AudioManager.Instance.UISfx())
        {
            _onOffUISfxBtn.text = "OFF";
            _onOffUISfxBtn.style.color = Color.white;
            _onOffUISfxBtn.style.unityBackgroundImageTintColor = new Color(0.518f, 0.518f, 0.518f, 1);
        }
        else
        {
            _onOffUISfxBtn.text = "ON";
            _onOffUISfxBtn.style.color = Color.black;
            _onOffUISfxBtn.style.unityBackgroundImageTintColor = new Color(1, 1, 1, 1);
        }
    }

    void Update()
    {
        if (GameManager.Instance.IsPlaying)
        {
            _homePanel.style.display = DisplayStyle.None;
            _gamePlayUI.style.display = DisplayStyle.Flex;

            _scoreTimer += Time.deltaTime * 8;
            _score = Mathf.FloorToInt(_scoreTimer);
            _scoreText.text = _score.ToString();
        }

        if (_homePanel.style.display == DisplayStyle.None)
        {
            GameManager.Instance.IsStartGame = false;
        }

        if (GameManager.Instance.IsPauseGame)
        {
            _pauseGamePanel.RemoveFromClassList("PanelHidden");
            _currentScoreText.text = _scoreText.text;
            SaveBestScore();
        }
        else
        {
            _pauseGamePanel.AddToClassList("PanelHidden");
        }
        
        if (GameManager.Instance.IsGameOver)
        {
            _gameOverPanel.RemoveFromClassList("PanelHidden");
            SaveBestScore();
        }
    }

    private void SaveBestScore()
    {
        if (_score > PlayerPrefs.GetInt("Best Score", 0))
        {
            PlayerPrefs.SetInt("Best Score", _score);
            PlayerPrefs.Save();

            _bestScoreTextInPause.text = _score.ToString();
            _highScoreTextInGameOver.text = _score.ToString();
        }
    }

    private void OnClickOptions(ClickEvent evt)
    {
        AudioManager.Instance.ClickedSound();

        if (_optionsPanel.ClassListContains("OptionsPanelActive"))
        {
            _optionsPanel.RemoveFromClassList("OptionsPanelActive");
        }
        else
        {
            _optionsPanel.AddToClassList("OptionsPanelActive");
        }
    }

    private void OnClickPlay(ClickEvent evt)
    {
        AudioManager.Instance.ClickedSound();
        GameManager.Instance.IsStartGame = true;
        _playButton.style.display = DisplayStyle.None;
        _optionsButton.style.display = DisplayStyle.None;
    }

    private void OnClickPauseGame(ClickEvent evt)
    {
        AudioManager.Instance.ClickedSound();
        GameManager.Instance.PauseGame();
    }

    private void OnClickRestart(ClickEvent evt)
    {
        AudioManager.Instance.ClickedSound();
        GameManager.Instance.RestartGame();
    }

    private void OnClickSettings(ClickEvent evt)
    {
        AudioManager.Instance.ClickedSound();

        if (_settingsPanel.ClassListContains("PanelHidden"))
        {
            _settingsPanel.RemoveFromClassList("PanelHidden");
        }
        else
        {
            _settingsPanel.AddToClassList("PanelHidden");
        }
    }

    private void OnClickMusic(ClickEvent evt)
    {
        AudioManager.Instance.ClickedSound();
        AudioManager.Instance.OnOffMusic();

        if (_onOffMusicBtn.text == "OFF")
        {
            _onOffMusicBtn.text = "ON";
            _onOffMusicBtn.style.color = Color.black;
            _onOffMusicBtn.style.unityBackgroundImageTintColor = new Color(1, 1, 1, 1);
        }
        else if (_onOffMusicBtn.text == "ON")
        {
            _onOffMusicBtn.text = "OFF";
            _onOffMusicBtn.style.color = Color.white;
            _onOffMusicBtn.style.unityBackgroundImageTintColor = new Color(0.518f, 0.518f, 0.518f, 1);
        }
    }

    private void OnClickSfx(ClickEvent evt)
    {
        AudioManager.Instance.ClickedSound();
        AudioManager.Instance.OnOffSfx();

        if (_onOffSfxBtn.text == "OFF")
        {
            _onOffSfxBtn.text = "ON";
            _onOffSfxBtn.style.color = Color.black;
            _onOffSfxBtn.style.unityBackgroundImageTintColor = new Color(1, 1, 1, 1);
        }
        else if (_onOffSfxBtn.text == "ON")
        {
            _onOffSfxBtn.text = "OFF";
            _onOffSfxBtn.style.color = Color.white;
            _onOffSfxBtn.style.unityBackgroundImageTintColor = new Color(0.518f, 0.518f, 0.518f, 1);
        }
    }

    private void OnClickUISfx(ClickEvent evt)
    {
        AudioManager.Instance.ClickedSound();
        AudioManager.Instance.OnOffUISfx();

        if (_onOffUISfxBtn.text == "OFF")
        {
            _onOffUISfxBtn.text = "ON";
            _onOffUISfxBtn.style.color = Color.black;
            _onOffUISfxBtn.style.unityBackgroundImageTintColor = new Color(1, 1, 1, 1);
        }
        else if (_onOffUISfxBtn.text == "ON")
        {
            _onOffUISfxBtn.text = "OFF";
            _onOffUISfxBtn.style.color = Color.white;
            _onOffUISfxBtn.style.unityBackgroundImageTintColor = new Color(0.518f, 0.518f, 0.518f, 1);
        }
    }
}