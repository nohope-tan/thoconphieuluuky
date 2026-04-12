using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]
public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 7f;
    public float jumpForce = 14f;

    [Header("Ground Check")]
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;

    [Header("Jump Settings")]
    public float coyoteTime = 0.2f;
    public float jumpBufferTime = 0.2f;
    private float coyoteTimeCounter, jumpBufferCounter;
    private int jumpCount;

    [Header("Respawn & Death")]
    public Transform spawnPoint; // Kéo Empty Object làm điểm hồi sinh vào đây
    public float fallDeathY = -10f;

    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private float horizontalInput;
    private bool isGrounded;
    private string currentState;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Nếu quên gán spawnPoint, lấy vị trí lúc Start làm điểm hồi sinh
        if (spawnPoint == null)
        {
            GameObject go = new GameObject("DefaultSpawnPoint");
            go.transform.position = transform.position;
            spawnPoint = go.transform;
        }
    }

    void Update()
    {
        // 1. Kiểm tra rơi vực
        if (transform.position.y <= fallDeathY)
        {
            Respawn();
        }

        CheckGrounded();
        horizontalInput = Input.GetAxisRaw("Horizontal");


        FlipCharacter();

        // 3. Coyote Time & Jump Buffer
        if (isGrounded) coyoteTimeCounter = coyoteTime;
        else coyoteTimeCounter -= Time.deltaTime;

        if (Input.GetButtonDown("Jump")) jumpBufferCounter = jumpBufferTime;
        else jumpBufferCounter -= Time.deltaTime;

        // 4. Nhảy 2 lần (1 lần dưới đất + 1 lần trên không)
        if (jumpBufferCounter > 0f && (coyoteTimeCounter > 0f || jumpCount < 2))
        {
            // Nếu dùng coyote time (lần nhảy đầu), không tính vào jumpCount
            if (coyoteTimeCounter > 0f && jumpCount == 0)
            {
                Jump();
            }
            else if (jumpCount < 2)
            {
                Jump();
            }
            jumpBufferCounter = 0f;
            coyoteTimeCounter = 0f;
        }

        UpdateAnimations();
    }

    void FixedUpdate()
    {
        // Điều khiển ngang trực tiếp cả dưới đất lẫn trên không
        rb.linearVelocity = new Vector2(horizontalInput * moveSpeed, rb.linearVelocity.y);
    }

    void CheckGrounded()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
        if (isGrounded) jumpCount = 0;
    }

    void Jump()
    {
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        jumpCount++;
        isGrounded = false;
    }

    void FlipCharacter()
    {
        if (horizontalInput > 0.01f) spriteRenderer.flipX = false;
        else if (horizontalInput < -0.01f) spriteRenderer.flipX = true;
    }

    public void Respawn()
    {
        transform.position = spawnPoint.position;
        rb.linearVelocity = Vector2.zero; // Xóa lực quán tính để không bị trượt khi hồi sinh
        Debug.Log("Player Respawned!");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Respawn();
        }
    }

    void UpdateAnimations()
    {
        if (isGrounded)
            ChangeAnimationState(Mathf.Abs(horizontalInput) > 0.01f ? "Run" : "Idle");
        else
            ChangeAnimationState("Jump");
    }

    void ChangeAnimationState(string newState)
    {
        if (currentState == newState) return;
        animator.Play(newState);
        currentState = newState;
    }
}