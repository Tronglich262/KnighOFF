using UnityEngine;
using TMPro;
using System;
using System.Collections;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;
    public TextMeshProUGUI scoreText;
    public int currentScore;

    public event Action OnScoreChanged; 


    [Header("Coin Multiplier")]
    public int coinMultiplier = 1;
    public int CoinMultiplierx5 = 5;
    public int currencoin;
   
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); 
            LoadScore(); 
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        currencoin = coinMultiplier;
        UpdateScoreUI(); 
    }

    public void AddScore(int amount)
    {
        currentScore += amount * currencoin;
        SaveScore();
    }

    public bool SpendScore(int cost)
    {
        if (currentScore >= cost)
        {
            currentScore -= cost;
            SaveScore();
            return true;
        }
        return false;
    }

    public void ResetScore() 
    {
        currentScore = 0;
        SaveScore();
    }

    void SaveScore()
    {
        PlayerPrefs.SetInt("SavedScore", currentScore); 
        PlayerPrefs.Save();
        UpdateScoreUI();
        OnScoreChanged?.Invoke(); 
    }

    void LoadScore()
    {
        currentScore = PlayerPrefs.GetInt("SavedScore", 0); 
        UpdateScoreUI();
    }

    void UpdateScoreUI()
    {
        if (scoreText == null)
        {
            scoreText = FindObjectOfType<TextMeshProUGUI>(); 
        }

        if (scoreText != null)
        {
            scoreText.text = "Score: " + currentScore; 
        }
    }

    public int GetScore()
    {
        return currentScore;
    }

    void OnEnable()
    {
        LoadScore(); 
        PhanThuong.OnThuongTiaset += StartCoinX5;

    }
    public int resetScore()
    {
        currentScore = 0;
        SaveScore();
        return currentScore;
    }
    private void OnApplicationQuit()
    {
        resetScore();
    }

    //vòng quay x5 coin
    IEnumerator CoinX5()
    {
        currencoin = CoinMultiplierx5;
        yield return new WaitForSeconds(5f);
        currencoin = coinMultiplier;

    }
    public void StartCoinX5()
    {
        StartCoroutine(CoinX5());
    }

    private void OnDisable()
    {
        PhanThuong.OnThuongTiaset -= StartCoinX5;
    }

}