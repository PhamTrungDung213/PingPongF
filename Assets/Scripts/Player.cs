using UnityEngine;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour
{
    private Vector2 startPosition;

    [SerializeField] private Rigidbody2D rb;
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float timeToAutoPlay = 3f;
    private float idleTimer = 0f;
    private float deadZone = 0.7f;

    [Header("Input Settings")]
    [SerializeField] private KeyCode upKey;
    [SerializeField] private KeyCode downKey;
    private float moveDirection = 0f;
    private float moveSpeedMultiplier = 1f;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        startPosition = transform.position;
        GameManager.instance.onReset += resetPosition;
        GameManager.instance.gameUI.onStartGame += resetPosition;
    }

    private void resetPosition()
    {
        transform.position = startPosition;
    }

    private void MovePlayer()
    {
        float playerInput = Input.GetKey(upKey) ? 1f : (Input.GetKey(downKey) ? -1f : 0f);
        idleTimer = (playerInput != 0f) ? 0f : idleTimer + Time.deltaTime;

        if (playerInput == 0f && idleTimer >= timeToAutoPlay && GameManager.instance.IsAi())
        {
            if (Mathf.Abs(GameManager.instance.ball.transform.position.y - transform.position.y) > deadZone)
            {
                moveDirection = GameManager.instance.ball.transform.position.y > transform.position.y ? 1f : -1f;
                AiMove(moveDirection);
            }
            else
            {
                moveDirection = 0f;
            }
        }
        else
        {
            moveDirection = playerInput;
            Move(moveDirection);
        }
    }

    private void Move(float moveDirection)
    {
        Vector2 direction = new Vector2(0f, moveDirection);
        rb.MovePosition(transform.position + (Vector3)direction * moveSpeed * moveSpeedMultiplier * Time.deltaTime);
    }

    private void AiMove(float moveDirection)
    {
        if (Random.value < 0.01f)
        {
            moveSpeedMultiplier = Random.Range(0.5f, 1.5f);
        }
        Vector2 direction = new Vector2(0f, moveDirection);
        // transform.Translate(direction * moveSpeed * moveSpeedMultiplier * Time.deltaTime);
        rb.MovePosition(transform.position + (Vector3)direction * moveSpeed * moveSpeedMultiplier * Time.deltaTime);
    }

    void Update()
    {
        MovePlayer();
    }
}