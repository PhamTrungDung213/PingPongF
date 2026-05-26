using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class GameUI : MonoBehaviour
{
    [SerializeField] private GameObject menu;
    [SerializeField] public System.Action onStartGame;
    [SerializeField] public TextMeshProUGUI winText;

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
        winText.text = $"Player {winnerId} Wins!";
    }
}
