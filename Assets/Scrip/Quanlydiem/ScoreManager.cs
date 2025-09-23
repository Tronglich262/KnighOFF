using UnityEngine;
using TMPro;
using System;
using System.Collections;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;
    public TextMeshProUGUI scoreText;
    public int currentScore;

    public event Action OnScoreChanged; // Sự kiện khi điểm thay đổi


    [Header("Coin Multiplier")]
    public int coinMultiplier = 1;
    public int CoinMultiplierx5 = 5;
    public int currencoin;
   
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Giữ điểm khi đổi Scene
            LoadScore(); // Đọc điểm từ PlayerPrefs khi game khởi động
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        currencoin = coinMultiplier;
        UpdateScoreUI(); // Hiển thị điểm ngay lập tức khi vào game
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

    public void ResetScore() // Thêm chức năng Reset điểm
    {
        currentScore = 0;
        SaveScore();
    }

    void SaveScore()
    {
        PlayerPrefs.SetInt("SavedScore", currentScore); // Lưu điểm vào bộ nhớ
        PlayerPrefs.Save();
        UpdateScoreUI();
        OnScoreChanged?.Invoke(); // Gọi sự kiện cập nhật UI
    }

    void LoadScore()
    {
        currentScore = PlayerPrefs.GetInt("SavedScore", 0); // Đọc điểm đã lưu
        UpdateScoreUI();
    }

    void UpdateScoreUI()
    {
        if (scoreText == null)
        {
            scoreText = FindObjectOfType<TextMeshProUGUI>(); // Tìm lại UI khi load Scene mới
        }

        if (scoreText != null)
        {
            scoreText.text = "Score: " + currentScore; // Cập nhật UI ngay lập tức
        }
    }

    public int GetScore()
    {
        return currentScore;
    }

    void OnEnable()
    {
        LoadScore(); // Đảm bảo điểm hiển thị ngay khi Scene mới tải
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