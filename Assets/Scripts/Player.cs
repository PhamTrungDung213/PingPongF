using UnityEngine;

public class Player : MonoBehaviour
{
    private Vector2 startPosition;

    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float timeToAutoPlay = 120f;
    private float idleTimer = 0f;
    private float deadZone = 0.7f;

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

    private void Move()
    {
        float moveDirection = 0f;

        if (Input.GetKey(upKey))
        {
            moveDirection = 1f;
            idleTimer = 0f;
        }
        else if (Input.GetKey(downKey))
        {
            moveDirection = -1f;
            idleTimer = 0f;
        }
        else
        {
            idleTimer++;
        }

        if(moveDirection == 0f && idleTimer >= timeToAutoPlay && GameManager.instance.IsAi())
        {
            if(Mathf.Abs(GameManager.instance.ball.transform.position.y - transform.position.y) > deadZone)
            {
                moveDirection = GameManager.instance.ball.transform.position.y > transform.position.y ? 1f : -1f;
            }
        }

        Vector2 direction = new Vector2(0f, moveDirection);
        transform.Translate(direction * moveSpeed * Time.deltaTime);
    }

    void Update()
    {
        Move();
    }
}