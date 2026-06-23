using UnityEngine;
using TMPro; 
using System.Collections;











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

    
    private readonly Color bronzeColor = new Color(0.80f, 0.50f, 0.20f); 
    private readonly Color silverColor = new Color(0.75f, 0.75f, 0.80f); 
    private readonly Color goldColor   = new Color(1.00f, 0.84f, 0.00f); 

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
        
        if (popupText != null)
            popupText.gameObject.SetActive(false);

        RefreshAll();

        
        if (ScoreManager.Instance != null)
            ScoreManager.Instance.OnScoreChanged += OnScoreChanged;
    }

    
    void OnScoreChanged(int total, CoinType type, int pointsAdded)
    {
        RefreshAll();

        if (pointsAdded > 0)
            ShowPopup(type, pointsAdded);
    }

    
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

    
    void ShowPopup(CoinType type, int points)
    {
        if (popupText == null) return;

        if (popupCoroutine != null)
            StopCoroutine(popupCoroutine);

        popupCoroutine = StartCoroutine(PopupRoutine(type, points));
    }

    IEnumerator PopupRoutine(CoinType type, int points)
    {
        
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

            
            rect.anchoredPosition = Vector2.Lerp(startPos, endPos, t);

            
            float alpha = t < 0.5f ? 1f : Mathf.Lerp(1f, 0f, (t - 0.5f) / 0.5f);
            popupText.color = new Color(color.r, color.g, color.b, alpha);

            yield return null;
        }

        
        rect.anchoredPosition = startPos;
        popupText.gameObject.SetActive(false);
        popupCoroutine = null;
    }
}

