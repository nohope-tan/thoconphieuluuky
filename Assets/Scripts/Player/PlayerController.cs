using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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
    public Transform spawnPoint;
    public float fallDeathY = -10f;

    [Header("Health & Lives")]
    public int maxLives = 3;
    private int currentLives;

    [Header("UI References")]
    [Tooltip("Thả TextMeshPro hiển thị số máu vào đây")]
    public TextMeshProUGUI livesText;
    [Tooltip("Thả UI Text (Legacy) hiển thị số máu vào đây (nếu dùng Text thường)")]
    public Text legacyLivesText;
    [Tooltip("Thả Panel Game Over vào đây")]
    public GameObject gameOverPanel;
    [Tooltip("Thả Panel Win vào đây")]
    public GameObject winPanel;

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

        if (spawnPoint == null)
        {
            GameObject go = new GameObject("DefaultSpawnPoint");
            go.transform.position = transform.position;
            spawnPoint = go.transform;
        }

        currentLives = maxLives;
        UpdateLivesUI();
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(false);
        }
        if (winPanel != null)
        {
            winPanel.SetActive(false);
        }
    }

    void Update()
    {
        if (transform.position.y <= fallDeathY) TakeDamage();

        CheckGrounded();
        horizontalInput = Input.GetAxisRaw("Horizontal");
        FlipCharacter();

        if (isGrounded) coyoteTimeCounter = coyoteTime;
        else coyoteTimeCounter -= Time.deltaTime;

        if (Input.GetButtonDown("Jump")) jumpBufferCounter = jumpBufferTime;
        else jumpBufferCounter -= Time.deltaTime;

        if (jumpBufferCounter > 0f)
        {
            if (coyoteTimeCounter > 0f)
            {
                Jump();
                jumpCount = 1;
                jumpBufferCounter = 0f;
                coyoteTimeCounter = 0f;
            }
            else if (jumpCount < 2)
            {
                Jump();
                jumpCount = 2;
                jumpBufferCounter = 0f;
            }
        }

        UpdateAnimations();
    }

    void FixedUpdate()
    {
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
        rb.linearVelocity = Vector2.zero;
    }

    public void TakeDamage()
    {
        if (currentLives <= 0) return;

        currentLives--;
        UpdateLivesUI();

        if (currentLives <= 0)
        {
            GameOver();
        }
        else
        {
            Respawn();
        }
    }

    private void GameOver()
    {
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
        }

        rb.linearVelocity = Vector2.zero;
        rb.bodyType = RigidbodyType2D.Static;
        this.enabled = false;
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void UpdateLivesUI()
    {
        if (livesText != null)
        {
            livesText.text = currentLives.ToString();
        }
        if (legacyLivesText != null)
        {
            legacyLivesText.text = currentLives.ToString();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy")) TakeDamage();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Fin"))
        {
            spawnPoint.position = collision.transform.position;
            collision.enabled = false;
        }
        else if (collision.CompareTag("carot") || collision.CompareTag("Carot"))
        {
            WinGame();
        }
    }

    private void WinGame()
    {
        if (winPanel != null)
        {
            winPanel.SetActive(true);
        }

        rb.linearVelocity = Vector2.zero;
        rb.bodyType = RigidbodyType2D.Static;
        this.enabled = false;
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
