using UnityEngine;

public class BaseSpike : MonoBehaviour
{
    // Xử lý khi Spike chạm vào Player (nếu Spike có Collider dạng Solid)
    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        PlayerController player = collision.gameObject.GetComponent<PlayerController>();
        if (player != null)
        {
            player.Respawn();
        }
    }

    // Xử lý khi Spike chạm vào Player (nếu Spike có Collider dạng Trigger)
    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerController player = collision.GetComponent<PlayerController>();
        if (player != null)
        {
            player.Respawn();
        }
    }
}
