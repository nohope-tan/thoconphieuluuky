using UnityEngine;
using System;





public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }

    
    public int bronzeValue = 1;   
    public int silverValue = 5;   
    public int goldValue   = 10;  

    
    public int BronzeCount { get; private set; }
    public int SilverCount { get; private set; }
    public int GoldCount   { get; private set; }

    
    public int TotalScore  { get; private set; }

    
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

    
    public void ResetScore()
    {
        BronzeCount = SilverCount = GoldCount = TotalScore = 0;
        OnScoreChanged?.Invoke(0, CoinType.Bronze, 0);
    }
}

