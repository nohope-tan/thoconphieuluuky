using UnityEngine;

/// <summary>
/// Gắn vào từng Coin GameObject.
/// Yêu cầu: Collider2D ở chế độ IsTrigger = true.
/// Tag của GameObject phải khớp với một trong:
///   "coin-đồng" | "Coin1-bạc" | "Coin2-vàng"
/// </summary>
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
        // Tự động gán CoinType dựa theo Tag để tránh sai sót
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

        // Đảm bảo Collider là trigger
        GetComponent<Collider2D>().isTrigger = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (collected) return;
        
        // Nhận diện Player (có thể thông qua PlayerController hoặc Tag Player)
        if (other.GetComponent<PlayerController>() != null || other.CompareTag("Player"))
        {
            collected = true; // Khóa lại ngay lập tức
            Collect();
        }
    }

    void Collect()
    {
        // Cộng điểm
        if (ScoreManager.Instance != null)
            ScoreManager.Instance.AddCoin(coinType);

        // Phát effect và âm thanh (nếu có)
        if (collectEffectPrefab != null) Instantiate(collectEffectPrefab, transform.position, Quaternion.identity);
        if (collectSound != null) AudioSource.PlayClipAtPoint(collectSound, transform.position);

        // TẮT NGAY LẬP TỨC ĐỂ TRÁNH VA CHẠM THÊM VÀ BIẾN MẤT KHỎI MÀN HÌNH
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        if (sr != null) sr.enabled = false;
        
        Collider2D col = GetComponent<Collider2D>();
        if (col != null) col.enabled = false;

        // Xóa hoàn toàn vật thể
        Destroy(gameObject);
    }
}
