using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class GameUI : MonoBehaviour
{
    [SerializeField] private GameObject menu;
    [SerializeField] public System.Action onStartGame;
    [SerializeField] public TextMeshProUGUI winText;
    [SerializeField] public TextMeshProUGUI switchModeButtonText;
    [SerializeField] public TextMeshProUGUI volumeValue;
    [SerializeField] public GameObject startButton;
    [SerializeField] private GameObject playModeButton;
    private bool isPaused = false;

    private void Start()
    {
        AdjustPlayModeButtonText();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ContinueGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void PauseGame()
    {
        isPaused = true;
        Time.timeScale = 0f; // Đóng băng toàn bộ thời gian trong game (Bóng và Player sẽ dừng lại)
        menu.SetActive(true);
        playModeButton.SetActive(false);
        startButton.SetActive(false);
        winText.text = "PAUSE\nEsc again to resume!";
    }

    public void ContinueGame()
    {
        isPaused = false;
        Time.timeScale = 1f; // Khôi phục lại thời gian bình thường để tiếp tục chơi
        menu.SetActive(false);
    }

    public void OnStartGameButtonClicked()
    {
        menu.SetActive(false);
        onStartGame?.Invoke();
    }

    public void UpdateScores()
    {
        GameManager.instance.scoreTextP1.SetScore(GameManager.instance.scoreP1);
        GameManager.instance.scoreTextP2.SetScore(GameManager.instance.ScoreP2);
    }

    public void HightLightScore(int id)
    {
        if (id == 1)
        {
            GameManager.instance.scoreTextP1.HighLight();
        }
        else if (id == 2)
        {
            GameManager.instance.scoreTextP2.HighLight();
        }
    }

    public void OnGameEnd(int winnerId)
    {
        menu.SetActive(true);
        playModeButton.SetActive(true);
        startButton.SetActive(true);
        winText.text = $"Player {winnerId} Wins!";
    }

    public void SetVolume(float volume)
    {
        AudioListener.volume = volume;
        volumeValue.text = $"{Mathf.RoundToInt(volume * 100)}%";
    }

    public void AdjustPlayModeButtonText()
    {
        string s = string.Empty;

        switch(GameManager.instance.playMode)
        {
            case GameManager.PlayMode.PvP:
                s = "Player";
                break;
            case GameManager.PlayMode.Ai:
                s = "Ai";
                break;
        }

        switchModeButtonText.text = s;
    }

    public void OnSwitchModeButtonClicked()
    {
        GameManager.instance.SwitchPlayMode();
        AdjustPlayModeButtonText();
    }

    public void OnQuitButtonClicked()
    {
        Application.Quit();
    }
}
