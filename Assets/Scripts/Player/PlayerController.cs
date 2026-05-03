using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(AudioSource))]
public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 7f;
    public float jumpForce = 14f;

    [Header("Audio")]
    public AudioClip jumpSound;

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
    private AudioSource audioSource;
    private float horizontalInput;
    private bool isGrounded;
    private string currentState;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();

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
        if (jumpBufferCounter > 0f)
        {
            if (coyoteTimeCounter > 0f) // Nhảy lần đầu (từ mặt đất hoặc Coyote Time)
            {
                Jump();
                jumpCount = 1; // Đảm bảo tính là đã nhảy lần 1
                jumpBufferCounter = 0f;
                coyoteTimeCounter = 0f;
            }
            else if (jumpCount < 2) // Nhảy lần 2 (nhảy trên không)
            {
                Jump();
                jumpCount = 2; // Đánh dấu đã hết lượt nhảy (phải chạm đất mới reset)
                jumpBufferCounter = 0f;
            }
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

        // Phát âm thanh nhảy
        if (audioSource != null && jumpSound != null)
        {
            audioSource.PlayOneShot(jumpSound);
        }
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Fin"))
        {
            // Cập nhật vị trí hồi sinh thành vị trí của checkpoint "Fin"
            spawnPoint.position = collision.transform.position;
            Debug.Log("Checkpoint saved at: " + spawnPoint.position);

            // Tắt Collider của cột mốc này đi để không va chạm và lưu lại nhiều lần nữa
            collision.enabled = false;
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