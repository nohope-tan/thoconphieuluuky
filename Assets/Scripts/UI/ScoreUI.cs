using UnityEngine;
using TMPro; // Dùng cho TextMeshPro
using System.Collections;

/// <summary>
/// Gắn vào Canvas GameObject.
/// Hiển thị tổng điểm và số lượng từng loại coin.
/// Khi ăn coin sẽ hiện popup "+N" tại góc màn hình trong 1 giây.
/// 
/// Setup trong Unity:
///   1. Tạo Canvas (Screen Space - Overlay)
///   2. Gắn script này vào Canvas
///   3. Kéo các TextMeshPro - Text vào các slot tương ứng
/// </summary>
public class ScoreUI : MonoBehaviour
{
    [Header("Score Texts (TextMeshPro)")]
    [Tooltip("Text hiển thị tổng điểm")]
    public TextMeshProUGUI totalScoreText;

    [Tooltip("Text hiển thị số coin đồng đã ăn")]
    public TextMeshProUGUI bronzeCountText;

    [Tooltip("Text hiển thị số coin bạc đã ăn")]
    public TextMeshProUGUI silverCountText;

    [Tooltip("Text hiển thị số coin vàng đã ăn")]
    public TextMeshProUGUI goldCountText;

    [Header("Popup Settings")]
    [Tooltip("Text dùng để hiện '+N' khi ăn coin")]
    public TextMeshProUGUI popupText;

    [Tooltip("Thời gian popup tồn tại (giây)")]
    public float popupDuration = 1f;

    [Tooltip("Popup nổi lên bao nhiêu pixel")]
    public float popupRiseAmount = 60f;

    // Màu sắc popup theo loại coin
    private readonly Color bronzeColor = new Color(0.80f, 0.50f, 0.20f); // nâu đồng
    private readonly Color silverColor = new Color(0.75f, 0.75f, 0.80f); // bạc
    private readonly Color goldColor   = new Color(1.00f, 0.84f, 0.00f); // vàng

    private Coroutine popupCoroutine;

    void OnEnable()
    {
        if (ScoreManager.Instance != null)
            ScoreManager.Instance.OnScoreChanged += OnScoreChanged;
    }

    void OnDisable()
    {
        if (ScoreManager.Instance != null)
            ScoreManager.Instance.OnScoreChanged -= OnScoreChanged;
    }

    void Start()
    {
        // Ẩn popup ban đầu
        if (popupText != null)
            popupText.gameObject.SetActive(false);

        RefreshAll();

        // Đăng ký event (phòng trường hợp OnEnable chạy trước ScoreManager khởi tạo)
        if (ScoreManager.Instance != null)
            ScoreManager.Instance.OnScoreChanged += OnScoreChanged;
    }

    /// <summary>Callback khi ScoreManager thông báo điểm thay đổi.</summary>
    void OnScoreChanged(int total, CoinType type, int pointsAdded)
    {
        RefreshAll();

        if (pointsAdded > 0)
            ShowPopup(type, pointsAdded);
    }

    /// <summary>Cập nhật toàn bộ text từ ScoreManager.</summary>
    void RefreshAll()
    {
        if (ScoreManager.Instance == null) return;

        if (totalScoreText  != null)
            totalScoreText.text  = $"Điểm: {ScoreManager.Instance.TotalScore}";

        if (bronzeCountText != null)
            bronzeCountText.text = $"x{ScoreManager.Instance.BronzeCount}";

        if (silverCountText != null)
            silverCountText.text = $"x{ScoreManager.Instance.SilverCount}";

        if (goldCountText   != null)
            goldCountText.text   = $"x{ScoreManager.Instance.GoldCount}";
    }

    /// <summary>Hiện popup "+N" nổi lên rồi mờ dần.</summary>
    void ShowPopup(CoinType type, int points)
    {
        if (popupText == null) return;

        if (popupCoroutine != null)
            StopCoroutine(popupCoroutine);

        popupCoroutine = StartCoroutine(PopupRoutine(type, points));
    }

    IEnumerator PopupRoutine(CoinType type, int points)
    {
        // Chọn màu theo loại coin
        Color color = type == CoinType.Gold   ? goldColor
                    : type == CoinType.Silver ? silverColor
                    : bronzeColor;

        popupText.text  = $"+{points}";
        popupText.color = color;
        popupText.gameObject.SetActive(true);

        RectTransform rect = popupText.rectTransform;
        Vector2 startPos   = rect.anchoredPosition;
        Vector2 endPos     = startPos + Vector2.up * popupRiseAmount;

        float elapsed = 0f;
        while (elapsed < popupDuration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / popupDuration;

            // Di chuyển lên
            rect.anchoredPosition = Vector2.Lerp(startPos, endPos, t);

            // Mờ dần ở nửa sau
            float alpha = t < 0.5f ? 1f : Mathf.Lerp(1f, 0f, (t - 0.5f) / 0.5f);
            popupText.color = new Color(color.r, color.g, color.b, alpha);

            yield return null;
        }

        // Reset về vị trí ban đầu
        rect.anchoredPosition = startPos;
        popupText.gameObject.SetActive(false);
        popupCoroutine = null;
    }
}
