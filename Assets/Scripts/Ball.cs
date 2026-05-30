using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb2d;
    [SerializeField] public ParticleSystem hitEffect;
    [SerializeField] private float maxInitialAngle = 0.67f;

    [Header("Speed Settings")]
    [SerializeField] private float baseMoveSpeed = 5f;          
    [SerializeField] private float speedIncreasePerHit = 0.5f;  
    //public float maxSpeed = 15f;              
    [SerializeField] private float currentSpeed;               
    [SerializeField] private float startX = 0f;
    [SerializeField] private float maxStartY = 4f;

    private void Start()
    {
        GameManager.instance.onReset += ResetBall;
        GameManager.instance.gameUI.onStartGame += ResetBall;
    }

    private void InititalPush()
    {
        currentSpeed = baseMoveSpeed;

        Vector2 dir = Random.value < 0.5f ? Vector2.left : Vector2.right;
        dir.y = Random.Range(-maxInitialAngle, maxInitialAngle);

        rb2d.linearVelocity = dir.normalized * currentSpeed;

        HitEffect(30);
    }

    public void ResetBall()
    {
        float posY = Random.Range(-maxStartY, maxStartY);
        transform.position = new Vector2(startX, posY);
        InititalPush();
    }

    private void FixedUpdate()
    {
        // Kiểm tra an toàn: Đảm bảo bóng không bị đứng im hoàn toàn (lỗi chia cho 0)
        if (rb2d.linearVelocity.sqrMagnitude > 0.01f)
        {
            // Bất chấp lực cản/ma sát của Unity, code này liên tục bơm lại tốc độ chuẩn
            rb2d.linearVelocity = rb2d.linearVelocity.normalized * currentSpeed;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        ScoreZone scoreZone = collision.GetComponent<ScoreZone>();
        if (scoreZone)
        {
            GameManager.instance.OnScoreZoneReached(scoreZone.id);
            GameManager.instance.gameAudio.PlayScoreSound();
            GameManager.instance.screenShake.StartShake(0.3f, 0.1f);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            HitEffect(20);

            // Tăng tốc độ quả bóng
            currentSpeed += speedIncreasePerHit;

            // Tính toán góc nảy
            Vector2 ballPos = transform.position;
            Vector2 paddlePos = collision.transform.position;
            float paddleHeight = collision.collider.bounds.size.y;

            float y = (ballPos.y - paddlePos.y) / paddleHeight;
            float x = ballPos.x < paddlePos.x ? -1 : 1;

            // Áp dụng hướng mới kèm theo tốc độ vừa được tăng
            Vector2 newDirection = new Vector2(x, y).normalized;
            rb2d.linearVelocity = newDirection * currentSpeed;

            GameManager.instance.gameAudio.PlayHitSound();

            GameManager.instance.screenShake.StartShake(Mathf.Sqrt(rb2d.linearVelocity.magnitude)*0.08f, 0.05f);
        }

        if (collision.gameObject.CompareTag("Wall"))
        {
            HitEffect(10);

            GameManager.instance.gameAudio.PlayWallSound();

            GameManager.instance.screenShake.StartShake(0.03f, 0.03f);
        }
    }

    private void HitEffect(int amount)
    {
        hitEffect.Emit(amount);
    }
}