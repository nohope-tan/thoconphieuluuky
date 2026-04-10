using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyPatrol : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 2f;
    public bool movingRight = true;

    [Header("Detection (2 Tia Check)")]
    [Tooltip("Kéo một Object nằm ở CHÍNH GIỮA quái vào đây")]
    public Transform groundCheckCenter;

    [Tooltip("Khoảng cách 2 tia văng ra 2 bên trái/phải")]
    public float rayOffsetX = 0.5f;

    [Tooltip("Độ dài của tia cắm xuống đất")]
    public float rayDistance = 0.5f;
    public LayerMask groundLayer;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        // Cập nhật hướng quay mặt ngay khi bắt đầu (Giữ nguyên logic của bạn)
        UpdateFacingDirection();
    }

    void FixedUpdate()
    {
        // 1. Áp dụng vận tốc di chuyển
        float velocityX = movingRight ? moveSpeed : -moveSpeed;
        rb.linearVelocity = new Vector2(velocityX, rb.linearVelocity.y);

        // 2. Lấy vị trí gốc ở giữa
        Vector2 checkPos = groundCheckCenter.position;

        // Cộng thêm khoảng cách. Nếu đi phải -> dùng tia Phải. Đi trái -> dùng tia Trái.
        checkPos.x += movingRight ? rayOffsetX : -rayOffsetX;

        // 3. Kiểm tra vực (Raycast xuống)
        RaycastHit2D hit = Physics2D.Raycast(checkPos, Vector2.down, rayDistance, groundLayer);

        // 4. Nếu không chạm đất -> Đổi hướng
        if (hit.collider == null)
        {
            Flip();
        }
    }

    void Flip()
    {
        movingRight = !movingRight;
        UpdateFacingDirection();
    }

    void UpdateFacingDirection()
    {
        Vector3 newScale = transform.localScale;

        // Logic cũ của bạn: Đi phải thì Scale âm, đi trái thì Scale dương
        if (movingRight)
        {
            newScale.x = -Mathf.Abs(newScale.x);
        }
        else
        {
            newScale.x = Mathf.Abs(newScale.x);
        }

        transform.localScale = newScale;
    }

    // Vẽ luôn 2 tia trong Scene để bạn dễ dàng căn chỉnh bằng mắt
    private void OnDrawGizmos()
    {
        if (groundCheckCenter != null)
        {
            // Vẽ tia bên PHẢI (Màu Xanh lá)
            Gizmos.color = Color.green;
            Vector3 rightPos = groundCheckCenter.position + new Vector3(rayOffsetX, 0, 0);
            Gizmos.DrawLine(rightPos, rightPos + Vector3.down * rayDistance);

            // Vẽ tia bên TRÁI (Màu Xanh dương)
            Gizmos.color = Color.cyan;
            Vector3 leftPos = groundCheckCenter.position + new Vector3(-rayOffsetX, 0, 0);
            Gizmos.DrawLine(leftPos, leftPos + Vector3.down * rayDistance);
        }
    }
}