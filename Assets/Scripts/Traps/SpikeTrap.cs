using UnityEngine;

public class SpikeTrap : BaseSpike
{
    [Header("Cài đặt tia quét (Raycast)")]
    public float rayDistance = 5f; 
    public Vector2 attackDirection = Vector2.up; 
    public LayerMask playerLayer; 

    [Header("Cài đặt chuyển động của Spike")]
    public float attackDistance = 2f; 
    public float riseSpeed = 15f; 
    public float returnSpeed = 5f; 
    public float delayBeforeReturn = 1f; 

    private Vector3 originalPosition;
    private Vector3 targetPosition;
    private bool isTriggered = false;
    private bool returning = false;
    private float returnTimer;

    void Update()
    {
        Vector2 normalizedDirection = attackDirection.normalized;

        
        Debug.DrawRay(transform.position, normalizedDirection * rayDistance, Color.red);

        
        if (!isTriggered)
        {
            
            RaycastHit2D hit = Physics2D.Raycast(transform.position, normalizedDirection, rayDistance, playerLayer);
            
            if (hit.collider != null)
            {
                
                if (hit.collider.CompareTag("Player") || hit.collider.GetComponent<PlayerController>() != null)
                {
                    isTriggered = true; 
                    
                    originalPosition = transform.position;
                    targetPosition = originalPosition + (Vector3)normalizedDirection * attackDistance;
                }
            }
        }
        else 
        {
            if (!returning) 
            {
                
                transform.position = Vector3.MoveTowards(transform.position, targetPosition, riseSpeed * Time.deltaTime);

                
                if (Vector3.Distance(transform.position, targetPosition) < 0.01f)
                {
                    returnTimer += Time.deltaTime;
                    
                    if (returnTimer >= delayBeforeReturn)
                    {
                        returning = true;
                        returnTimer = 0f;
                    }
                }
            }
            else 
            {
                
                transform.position = Vector3.MoveTowards(transform.position, originalPosition, returnSpeed * Time.deltaTime);

                
                if (Vector3.Distance(transform.position, originalPosition) < 0.01f)
                {
                    isTriggered = false; 
                    returning = false;
                }
            }
        }
    }
}

