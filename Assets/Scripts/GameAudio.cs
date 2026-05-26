using UnityEngine;

public class GameAudio : MonoBehaviour
{
    [SerializeField] public AudioSource Sound;

    [Header("Audio Clips")]
    [SerializeField] public AudioClip wallSound;
    [SerializeField] public AudioClip hitSound;
    [SerializeField] public AudioClip scoreSound;
    [SerializeField] public AudioClip winSound;

    [Header("Volume Settings")]
    [Range(0f, 1f)] public float wallVolume = 1f;
    [Range(0f, 1f)] public float hitVolume = 1f;
    [Range(0f, 1f)] public float scoreVolume = 1f;
    [Range(0f, 1f)] public float winVolume = 1f;

    public void PlayWallSound()
    {
        Sound.PlayOneShot(wallSound, wallVolume);
    }

    public void PlayHitSound()
    {
        Sound.PlayOneShot(hitSound, hitVolume);
    }

    public void PlayScoreSound()
    {
        Sound.PlayOneShot(scoreSound, scoreVolume);
    }

    public void PlayWinSound()
    {
        Sound.PlayOneShot(winSound, winVolume);
    }
}
