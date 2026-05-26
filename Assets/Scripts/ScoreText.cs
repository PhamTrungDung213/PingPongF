using UnityEngine;
using TMPro;
using System;
using Unity.VisualScripting;

public class ScoreText : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private Animator animator;

    public void SetScore(int score)
    {
        text.text = score.ToString();
    }

    public void HighLight()
    {
        animator.SetTrigger("highlight");
    }

}
