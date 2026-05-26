using UnityEngine;

public class Player : MonoBehaviour
{
    private Vector2 startPosition;

    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 5f;

    [Header("Input Settings")]
    [SerializeField] private KeyCode upKey;
    [SerializeField] private KeyCode downKey;

    private void Start()
    {
        startPosition = transform.position;
        GameManager.instance.onReset += resetPosition;
        GameManager.instance.gameUI.onStartGame += resetPosition;
    }

    private void resetPosition()
    {
        transform.position = startPosition;
    }

    void Update()
    {
        float moveDirection = 0f;

        if (Input.GetKey(upKey))
        {
            moveDirection = 1f;
        }
        else if (Input.GetKey(downKey))
        {
            moveDirection = -1f;
        }

        Vector2 direction = new Vector2(0f, moveDirection);
        transform.Translate(direction * moveSpeed * Time.deltaTime);

    }
}