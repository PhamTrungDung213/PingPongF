using UnityEngine;
using System.Collections;

public class Shake : MonoBehaviour
{
    [SerializeField] private Vector3 initialPosition;

    private void Awake()
    {
        initialPosition = transform.localPosition;
    }

    public void StartShake(float maxOffset, float duration)
    {
        StopShake();
        StartCoroutine(ShakeSequence(maxOffset, duration));
    }

    public void StopShake()
    {
        StopAllCoroutines();
        transform.localPosition = initialPosition;
    }

    private IEnumerator ShakeSequence(float maxOffset, float duration)
    {
        float durationPass = 0f;
        while (durationPass < duration)
        {
            DoShake(maxOffset);
            durationPass += Time.deltaTime;
            yield return null;
        }
        transform.localPosition = initialPosition;
    }

    private void DoShake(float maxOffset)
    {
        float xOffset = Random.Range(-maxOffset, maxOffset);
        float yOffset = Random.Range(-maxOffset, maxOffset);
        transform.localPosition = initialPosition + new Vector3(xOffset, yOffset, 0f);
    }
}
