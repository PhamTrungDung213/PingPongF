using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] private int scoreP1, ScoreP2;
    [SerializeField] private ScoreText scoreTextP1, scoreTextP2;
    [SerializeField] public System.Action onReset;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void OnScoreZoneReached(int id)
    {
        onReset?.Invoke();

        if (id == 1)
        {
            scoreP1++;
        }
        else if (id == 2)
        {
            ScoreP2++;
        }

        UpdateScores();
    }

    private void UpdateScores()
    {
        scoreTextP1.SetScore(scoreP1);
        scoreTextP2.SetScore(ScoreP2);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
