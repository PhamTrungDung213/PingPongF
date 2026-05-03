using UnityEngine;
using TMPro;
using System;
using Unity.VisualScripting;

public class ScoreText : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI text;

    public void SetScore(int score)
    {
        text.text = score.ToString();
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
