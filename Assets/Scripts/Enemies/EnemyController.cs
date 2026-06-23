using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyPatrol : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 2f;
    public bool movingRight = true;

    [Header("Detection (2 Tia Check)")]
    [Tooltip("Kéo m?t Object n?m ? CHÍNH GI?A quái vào dây")]
    public Transform groundCheckCenter;

    [Tooltip("Kho?ng cách 2 tia vang ra 2 bên trái/ph?i")]
    public float rayOffsetX = 0.5f;

    [Tooltip("Ð? dài c?a tia c?m xu?ng d?t")]
    public float rayDistance = 0.5f;
    public LayerMask groundLayer;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        
        UpdateFacingDirection();
    }

    void FixedUpdate()
    {
        
        float velocityX = movingRight ? moveSpeed : -moveSpeed;
        rb.linearVelocity = new Vector2(velocityX, rb.linearVelocity.y);

        
        Vector2 checkPos = groundCheckCenter.position;

        
        checkPos.x += movingRight ? rayOffsetX : -rayOffsetX;

        
        RaycastHit2D hit = Physics2D.Raycast(checkPos, Vector2.down, rayDistance, groundLayer);

        
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

    
    private void OnDrawGizmos()
    {
        if (groundCheckCenter != null)
        {
            
            Gizmos.color = Color.green;
            Vector3 rightPos = groundCheckCenter.position + new Vector3(rayOffsetX, 0, 0);
            Gizmos.DrawLine(rightPos, rightPos + Vector3.down * rayDistance);

            
            Gizmos.color = Color.cyan;
            Vector3 leftPos = groundCheckCenter.position + new Vector3(-rayOffsetX, 0, 0);
            Gizmos.DrawLine(leftPos, leftPos + Vector3.down * rayDistance);
        }
    }
}
