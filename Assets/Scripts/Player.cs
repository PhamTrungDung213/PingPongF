using UnityEngine;

public class Player : MonoBehaviour
{
    // You can adjust this speed directly in the Unity Inspector
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 5f;

    [Header("Input Settings")]
    // Tạo biến để chọn phím trên Inspector
    [SerializeField] private KeyCode upKey;
    [SerializeField] private KeyCode downKey;

    void Update()
    {
        float moveDirection = 0f;

        // Kiểm tra xem phím Up hoặc Down (được gán trong Inspector) có đang được bấm không
        if (Input.GetKey(upKey))
        {
            moveDirection = 1f;
        }
        else if (Input.GetKey(downKey))
        {
            moveDirection = -1f;
        }

        // Áp dụng di chuyển (Ở đây ví dụ di chuyển lên xuống theo trục Y)
        // Nếu bạn làm game 3D di chuyển tới lui, hãy đổi thành new Vector3(0f, 0f, moveDirection)
        Vector2 direction = new Vector2(0f, moveDirection);
        transform.Translate(direction * moveSpeed * Time.deltaTime);


    }
}