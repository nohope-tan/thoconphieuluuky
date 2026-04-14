using UnityEngine;
using System;

/// <summary>
/// Singleton quản lý điểm số toàn cục của game.
/// Lắng nghe event OnScoreChanged để cập nhật UI.
/// </summary>
public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }

    // Điểm quy đổi cho từng loại coin
    public int bronzeValue = 1;   // đồng
    public int silverValue = 5;   // bạc
    public int goldValue   = 10;  // vàng

    // Số lượng từng loại đã ăn
    public int BronzeCount { get; private set; }
    public int SilverCount { get; private set; }
    public int GoldCount   { get; private set; }

    // Tổng điểm
    public int TotalScore  { get; private set; }

    // Event thông báo khi điểm thay đổi: (totalScore, coinType, pointsAdded)
    public event Action<int, CoinType, int> OnScoreChanged;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    /// <summary>Gọi khi player ăn một coin.</summary>
    public void AddCoin(CoinType type)
    {
        int points = 0;
        switch (type)
        {
            case CoinType.Bronze:
                points = bronzeValue;
                BronzeCount++;
                break;
            case CoinType.Silver:
                points = silverValue;
                SilverCount++;
                break;
            case CoinType.Gold:
                points = goldValue;
                GoldCount++;
                break;
        }

        TotalScore += points;
        OnScoreChanged?.Invoke(TotalScore, type, points);

        Debug.Log($"[Score] +{points} ({type}) | Tổng: {TotalScore}");
    }

    /// <summary>Reset toàn bộ điểm (dùng khi restart).</summary>
    public void ResetScore()
    {
        BronzeCount = SilverCount = GoldCount = TotalScore = 0;
        OnScoreChanged?.Invoke(0, CoinType.Bronze, 0);
    }
}
