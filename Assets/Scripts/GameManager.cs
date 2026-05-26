using System;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] public static GameManager instance;

    [SerializeField] public GameUI gameUI;
    [SerializeField] public GameAudio gameAudio;
    [SerializeField] public int scoreP1, ScoreP2;
    [SerializeField] public ScoreText scoreTextP1, scoreTextP2;
    [SerializeField] public Action onReset;
    [SerializeField] public int maxScore;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            gameUI.onStartGame += OnStartGame;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        gameUI.onStartGame -= OnStartGame;
    }

    public void OnScoreZoneReached(int id)
    {

        if (id == 1)
        {
            scoreP1++;
        }
        else if (id == 2)
        {
            ScoreP2++;
        }

        gameUI.UpdateScores();
        gameUI.HightLightScore(id);
        checkWin();
    }

    private void checkWin()
    {
        int winnerId = scoreP1==maxScore ? 1 : ScoreP2 == maxScore ? 2 : 0;
        if (winnerId != 0)
        {
            gameUI.OnGameEnd(winnerId);
            instance.gameAudio.PlayWinSound();
        }
        else
        {
            onReset?.Invoke();
        }
    }

    private void OnStartGame()
    {
        scoreP1 = 0;
        ScoreP2 = 0;
        gameUI.UpdateScores();
    }
}
