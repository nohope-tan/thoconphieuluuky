using UnityEngine;







[RequireComponent(typeof(Collider2D))]
public class Coin : MonoBehaviour
{
    [Header("Coin Settings")]
    [Tooltip("Loại coin — phải khớp với Tag đặt trong Unity")]
    public CoinType coinType = CoinType.Bronze;

    [Header("Collect Effect")]
    [Tooltip("(Tuỳ chọn) Particle effect khi ăn coin")]
    public GameObject collectEffectPrefab;

    [Tooltip("(Tuỳ chọn) Âm thanh khi ăn coin")]
    public AudioClip collectSound;

    private bool collected = false;

    void Start()
    {
        
        switch (gameObject.tag)
        {
            case "coin-đồng":
                coinType = CoinType.Bronze;
                break;
            case "Coin1-bạc":
                coinType = CoinType.Silver;
                break;
            case "Coin2-vàng":
                coinType = CoinType.Gold;
                break;
            default:
                Debug.LogWarning($"[Coin] Tag không hợp lệ: '{gameObject.tag}'. " +
                                 "Dùng: 'coin-đồng', 'Coin1-bạc', hoặc 'Coin2-vàng'");
                break;
        }

        
        GetComponent<Collider2D>().isTrigger = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (collected) return;
        
        
        if (other.GetComponent<PlayerController>() != null || other.CompareTag("Player"))
        {
            collected = true; 
            Collect();
        }
    }

    void Collect()
    {
        
        if (ScoreManager.Instance != null)
            ScoreManager.Instance.AddCoin(coinType);

        
        if (collectEffectPrefab != null) Instantiate(collectEffectPrefab, transform.position, Quaternion.identity);
        if (collectSound != null) AudioSource.PlayClipAtPoint(collectSound, transform.position);

        
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        if (sr != null) sr.enabled = false;
        
        Collider2D col = GetComponent<Collider2D>();
        if (col != null) col.enabled = false;

        
        Destroy(gameObject);
    }
}

